using System;
using System.Collections.Generic;
using System.Data;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
    public class IngredientsRepository
    {
        private readonly IDbConnection _db;

        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Ingredient GetById(int id)
        {
            string sql = "SELECT * FROM ingredients WHERE id = @id;";
            return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
        }

        internal IEnumerable<Ingredient> GetByRecipeId(int id)
        {
            string sql = @"
      SELECT 
      r.*,
      recip.*
      FROM ingredients r 
      JOIN recipes recip ON r.recipeId = recip.id
      WHERE recipeId = @id;";


            return _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
            {
                ingredient.Recipe = recipe;
                return ingredient;
            }, new { id }, splitOn: "id");
        }

        internal Ingredient Create(Ingredient newIngredient)
        {
            string sql = @"
      INSERT INTO ingredients
      (name, amount, recipeId)
      VALUES
      (@Name, @Amount, @RecipeId);
      SELECT LAST_INSERT_ID();
      ";
            int id = _db.ExecuteScalar<int>(sql, newIngredient);
            newIngredient.Id = id;
            return newIngredient;
        }

        internal Ingredient Edit(Ingredient update)
        {
            string sql = @"
      UPDATE ingredients
      SET
       amount = @Amount,
       name = @Name
      WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM ingredients WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}