using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _repo;

        public IngredientsService(IngredientsRepository repo)
        {
            _repo = repo;
        }

        internal Ingredient GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal IEnumerable<Ingredient> GetByRecipeId(int id)
        {
            IEnumerable<Ingredient> data = _repo.GetByRecipeId(id);
            return data;
        }


        internal Ingredient Create(Ingredient newProd)
        {
            return _repo.Create(newProd);
        }

        internal Ingredient Edit(Ingredient updated)
        {
            var data = GetById(updated.Id);
            updated.Name = updated.Name != null ? updated.Name : data.Name;
            updated.Amount = updated.Amount != null && updated.Amount.Length > 2 ? updated.Amount : data.Amount;
            return _repo.Edit(updated);
        }
        internal string Delete(int id)
        {
            GetById(id);
            _repo.Delete(id);
            return "delorted";
        }
    }
}