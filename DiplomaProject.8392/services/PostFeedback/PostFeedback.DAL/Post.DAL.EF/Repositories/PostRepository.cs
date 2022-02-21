
using DAL.Base.Contracts;
using Microsoft.EntityFrameworkCore;
using PostFeedback.DAL.EF.Data;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.DAL.EF.Repositories
{
    //Repository specific to post entity in order to implement proper many-to-many relationship insert (for rules and facilities)
    public class PostRepository : IRepository<Post>
    {
        private readonly PostDbContext _context;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }
        private DbSet<Post> _dbSet => _context.Set<Post>();
        //bulk insert of posts
        public async Task AddRangeAsync(ICollection<Post> items)
        {
            _dbSet.AddRange(items);
            await _context.SaveChangesAsync();
        }
        //insert post with attached related entities
        public async Task CreateAsync(Post entity)
        {
            //attach related entities to let ef core know that they exist in the database
            _context.AttachRange(entity.Rules);
            _context.AttachRange(entity.Facilities);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }
        //delete post
        public async Task DeleteAsync(long id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        //check if post exists in the database
        public bool DoesItemWithIdExist(long id)
        {
            return _dbSet.Any(t => t.Id == id);
        }
        //get all posts
        public async Task<ICollection<Post>> GetAllAsync(params Expression<Func<Post, object>>[] includes)
        {
            return await GetDbSetWithRelatedTables(includes).AsNoTracking().ToListAsync();
        }
        //get post by id
        public async Task<Post> GetByIdAsync(long id, params Expression<Func<Post, object>>[] includes)
        {
            return await GetDbSetWithRelatedTables(includes).AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
        }
        //get filtered posts
        public async Task<ICollection<Post>> GetFilteredAsync(Expression<Func<Post, bool>> filter, params Expression<Func<Post, object>>[] includes)
        {
            return await GetDbSetWithRelatedTables(includes).Where(filter).AsNoTracking().ToListAsync();
        }
        //bulk delete of posts
        public async Task RemoveRangeAsync(ICollection<Post> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
        //update post with attached related entities
        public async Task UpdateAsync(Post entity)
        {
            //get existing post from database including rules and facilities
            var existingPost = await GetDbSetWithRelatedTables(x=>x.Rules, x=>x.Facilities)
                .Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            //attach list of rules to the post
            AttachItemsToPost(entity.Rules, existingPost.Rules);
            //attach list of facilities to the post
            AttachItemsToPost(entity.Facilities, existingPost.Facilities);
            //update post information
            existingPost.UpdateInfo(
               entity.Title, entity.Description, entity.OwnerId, entity.CategoryId, entity.CityId,
               entity.Address, entity.ContactNumber, entity.RoomsNo, entity.BathroomsNo,
               entity.BedsNo, entity.MaxGuestsNo, entity.SquareMeters, entity.Price,
               entity.IsWholeApartment, entity.MovingInTime, entity.MovingOutTime);
            //save changes in the database
            await _context.SaveChangesAsync();
        }
        //include entities related to post
        private IQueryable<Post> GetDbSetWithRelatedTables(params Expression<Func<Post, object>>[] includes)
        {
            IQueryable<Post> query = _dbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }

            return query;
        }
        //method that attaches items (rules, facilities) to post
        private void AttachItemsToPost<T>(
            ICollection<T> items,
            ICollection<T> postItems) where T : Item
        {
            //first delete from post collections those items that are not in list of items that should be added
            List<T> itemsToDelete = postItems.Where(x => items.All(y => y.Id != x.Id)).ToList();
            foreach (var item in itemsToDelete)
            {
                postItems.Remove(item);
            }
            //then from items that should be added select those that do not exist in post items collection
            //and add them to collection of post items
            List<T> itemsToAdd = items.Where(x => postItems.All(y => y.Id != x.Id)).ToList();
            //attach these entities to context for tracking
            _context.AttachRange(itemsToAdd);
            foreach (var item in itemsToAdd)
            {
                //add to the list of post items
                postItems.Add(item);
            }
        }
    }
}
