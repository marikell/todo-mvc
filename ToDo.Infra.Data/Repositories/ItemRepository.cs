using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;

namespace ToDo.Infra.Data.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly string connectionString;

    public ItemRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("ToDoDb");
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        IEnumerable<Item> result;
        var query = "select * from Items";
        using (var con = new SqlConnection(connectionString))
        {
            try
            {
                con.Open();
                result = await con.QueryAsync<Item>(query);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return result;
        }

        ;
    }

    public async Task AddAsync(Item item)
    {
        var query = "insert into Items(Id, Description, Done, CreatedAt) values(@Id, @Description, @Done, @CreatedAt)";
        using (var con = new SqlConnection(connectionString))
        {
            try
            {
                con.Open();
                await con.ExecuteAsync(query, item);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        ;
    }

    public async Task EditAsync(Item item)
    {
        var count = 0;
        var query = "update Items set Description = @Description, Done = @Done where id = @Id";
        using (var con = new SqlConnection(connectionString))
        {
            try
            {
                con.Open();
                count = await con.ExecuteAsync(query, item);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        ;
    }

    public async Task<Item> GetById(Guid id)
    {
        var query = "select * from Items where Id = @id";
        await using var con = new SqlConnection(connectionString);
        con.Open();
        return await con.QueryFirstOrDefaultAsync<Item>(query, new { id = id });
    }

    public async Task DeleteAsync(Guid id)
    {
        var query = "delete Items where Id = @Id";
        await using var con = new SqlConnection(connectionString);
        con.Open();
        await con.ExecuteAsync(query, new { Id = id });
    }

    public async Task UpdateAsync(Guid id, Item item)
    {
        var sqlStatement = @"
                            UPDATE Items 
                            SET  Done = @Done
                            WHERE Id = @Id";
        await using var con = new SqlConnection(connectionString);
        con.Open();
        await con.ExecuteAsync(sqlStatement, item);
    }
}