using FirestoreTest.Domain;
using FirestoreTest.repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstoreTest.services
{
    public class DuikerService
    {
        private DuikerDao dao;

        public DuikerService()
        {
            dao = new DuikerDao();
        }

        public void addDuiker()
        {
            dao.addDuiker();
        }

        public async Task<IEnumerable<Duiker>> getDuikers()
        {
            return await dao.getDuikers2();
        }
    }
}
