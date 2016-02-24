//
// Author:
//     Nicolas VERINAUD <n.verinaud@gmail.com>
//
// Copyright (c) 2016 Nicolas VERINAUD. All Rights Reserved.
//

namespace MaBoutique

// #r "../packages/FSharp.Data.2.2.5/lib/portable-net40+sl5+wp8+win8/FSharp.Data.dll"

module DAL =
    open System
    open System.IO
    open FSharp.Data

    type MaBoutique = JsonProvider<"Db.json", EmbeddedResource="MaBoutique, Db.json">

    type Client = {
        Id: int;
        Nom: string;
        Prenom: string;
    }

    type Commande = {
        ClientId: int;
        Date: DateTime;
    }

    let private data =
        use fs = new FileStream(__SOURCE_DIRECTORY__ + "/Db.json", FileMode.Open)
        MaBoutique.Load(fs)

    let private clientFromJSON (c: MaBoutique.Client) =
        { Id = c.Id; Nom = c.Nom; Prenom = c.Prenom }

    let private commandeFromJSON (c: MaBoutique.Commande) =
        { ClientId = c.ClientId; Date = c.Date }

    let getClients () = 
        data.Clients 
        |> Seq.map clientFromJSON

    let getCommandes () = 
        data.Commandes 
        |> Seq.map commandeFromJSON

    let findClient id = 
        data.Clients 
        |> Seq.where (fun c -> c.Id = id)
        |> Seq.map clientFromJSON

    let findCommandesForClient clientId = 
        data.Commandes 
        |> Seq.where (fun c -> c.ClientId = clientId)
        |> Seq.map commandeFromJSON

    type ICommandesRepository =
        abstract member GetCommandesForClient : int -> Commande seq

    type JSONCommandesRepository () =
        interface ICommandesRepository with
            member this.GetCommandesForClient id = findCommandesForClient id