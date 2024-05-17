using BlazorBootstrap;
using DocumentFormat.OpenXml.Office2010.Excel;
using LLMEducation.Data;
using LLMEducation.Data.Entity;
using LLMEducation.Pages.Quiz;
using Microsoft.EntityFrameworkCore;
using System;

namespace LLMEducation.Repos
{
    public class TestQRepo
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public TestQRepo(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public List<TestQ> GetAllTestQ()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var allContent = context.TestQs.ToList();
                return allContent;
            }
        }

        public async Task<TestQ> GetRandom()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var totalCount = await context.TestQs.CountAsync();

                var randomIndex = new Random().Next(0, totalCount);

                var randomTestQ = await context.TestQs
                    .Include(tq => tq.content)
                    .Skip(randomIndex)
                    .FirstOrDefaultAsync();

                return randomTestQ;
            }
        }

        public async Task<TestQ> SaveTestQ(TestQ testQ)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var addedTestQ = await context.TestQs.AddAsync(testQ);
                await context.SaveChangesAsync();
                return addedTestQ.Entity;
            }
        }

        public void SaveTestQ(List<TestQ> testQs)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.TestQs.AddRange(testQs);
                context.SaveChanges();
            }
        }

        public int CountTestsForContent(string contentid, ModelType modelType)
        {
            int response = 0;
            using (var context = _contextFactory.CreateDbContext())
            {
                response = context.TestQs.Where(x => x.contentId == contentid && x.modelType == modelType).Count();
            }

            return response;
        }

        internal async Task<TestQ?> GetRandomByContentAndModel(string id, ModelType modelType)
        {
            TestQ? randomTestQ = null;

            using (var context = _contextFactory.CreateDbContext())
            {
                var totalCount = await context.TestQs.Where(x => x.contentId == id && x.modelType == modelType).CountAsync();

                var randomIndex = new Random().Next(0, totalCount);

                randomTestQ = await context.TestQs
                    .Where(x => x.contentId == id && x.modelType == modelType)
                    .Skip(randomIndex)
                    .FirstOrDefaultAsync();
            }

            return randomTestQ;
        }

        internal async Task<List<TestQ>> GetTestQs(List<string> contentIds)
        {
            List<TestQ> result;

            using (var context = _contextFactory.CreateDbContext())
            {
                result = await context.TestQs
                    .Where(x => contentIds.Contains(x.contentId))
                    .Include(tq => tq.content)
                    .ToListAsync();
            }

            return result;
        }

        internal async Task<List<TestQ>> GetTestQs(string contentId)
        {
            List<TestQ> result;

            using (var context = _contextFactory.CreateDbContext())
            {
                result = await context.TestQs
                    .Where(x => contentId == x.contentId)
                    .Include(tq => tq.content)
                    .ToListAsync();
            }

            return result;
        }

    }
}
