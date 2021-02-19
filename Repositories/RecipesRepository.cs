using System;
using System.Collections.Generic;
using System.Data;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Recipe> GetAll()
        {
            string sql = "SELECT * FROM recipes;";
            return _db.Query<Recipe>(sql);
        }

        internal Recipe GetById(int id)
        {
            string sql = "SELECT * FROM recipes WHERE id = @id;";
            return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
        }

        internal Recipe Create(Recipe newRecipe)
        {
            string sql = @"
      INSERT INTO recipes
      (name, description)
      VALUES
      (@Name, @Description);
      SELECT LAST_INSERT_ID();
      ";
            int id = _db.ExecuteScalar<int>(sql, newRecipe);
            newRecipe.Id = id;
            return newRecipe;
        }

        internal Recipe Edit(Recipe update)
        {
            string sql = @"
      UPDATE FROM recipes
      SET
       description = @Description,
       name = @Name,
      WHERE id = @Id";
            _db.Execute(sql, update);
            return update;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}