//
// Author:
//     Nicolas VERINAUD <n.verinaud@gmail.com>
//
// Copyright (c) 2016 Nicolas VERINAUD. All Rights Reserved.
//
using System;
using System.Linq;
using System.Collections.Generic;

namespace MaBoutique
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			var commandesService = new CommandesService (new DAL.JSONCommandesRepository ());
			var commandes = commandesService.GetCommandesForClient (1);
			commandes.ToList ().ForEach (c => Console.WriteLine("{0} : {1}", c.ClientId, c.Date));
        }
    }

	public class CommandesService
	{
		private readonly DAL.ICommandesRepository _repository;

		public CommandesService(DAL.ICommandesRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<DAL.Commande> GetCommandesForClient(int clientId)
		{
			return _repository.GetCommandesForClient(clientId);
		}
	}
}
