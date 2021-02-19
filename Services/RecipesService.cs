using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Recipe> GetAll()
        {
            return _repo.GetAll();
        }

        internal Recipe GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal Recipe Create(Recipe newProd)
        {
            return _repo.Create(newProd);
        }

        internal Recipe Edit(Recipe updated)
        {
            // REVIEW
            var data = GetById(updated.Id);
            updated.Description = updated.Description != null ? updated.Description : data.Description;
            updated.Name = updated.Name != null && updated.Name.Length > 2 ? updated.Name : data.Name;
            return _repo.Edit(updated);
        }
        internal string Delete(int id)
        {
            var data = GetById(id);
            _repo.Delete(id);
            return "delorted";
        }
    }
}