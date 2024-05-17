using LLMEducation.Data;
using LLMEducation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LLMEducation.Repos
{
    public class ContentRepo
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ContentRepo(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<List<Content>> GetAllContent()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var allContent = await context.Contents.ToListAsync();
                return allContent;
            }
        }

        public async Task<Content?> GetContent(string id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var cont = await context.Contents.Where(e=> e.Id == id).FirstOrDefaultAsync();
                return cont;
            }
        }

        public async Task<List<Content>> GetAllContentBySubject(SubjectTypes subject)
        {
            string subjectStr = string.Empty;
            switch (subject)
            {
                case SubjectTypes.Biology:
                    subjectStr = "Biology";
                    break;
                case SubjectTypes.Physics:
                    subjectStr = "Physics";
                    break;
                case SubjectTypes.Chemistry:
                    subjectStr = "Chemistry";
                    break;
                default:
                    return new List<Content>();
            }

            using (var context = _contextFactory.CreateDbContext())
            {
                var allContent = await context.Contents.Where(e => e.Subject.ToLower().Equals(subjectStr.ToLower())).ToListAsync();
                return allContent;
            }
        }

        public async Task<Content> SaveContent(Content content)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var addedContent = await context.Contents.AddAsync(content);
                await context.SaveChangesAsync();
                return addedContent.Entity;
            }
        }


        public void SaveContent(List<Content> contents)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Contents.AddRange(contents);
                context.SaveChanges();
            }
        }

        public async Task DeleteContent(Content content)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var contentToDelete = await context.Contents.FindAsync(content.Id); 
                if (contentToDelete != null)
                {
                    context.Contents.Remove(contentToDelete);
                    await context.SaveChangesAsync();
                }
            }
        }


        internal async Task<Content> GetAllContentBySubjectRandom(SubjectTypes subject)
        {
            string subjectStr = string.Empty;
            switch (subject)
            {
                case SubjectTypes.Biology:
                    subjectStr = "Biology";
                    break;
                case SubjectTypes.Physics:
                    subjectStr = "Physics";
                    break;
                case SubjectTypes.Chemistry:
                    subjectStr = "Chemistry";
                    break;
                default:
                    return null;
            }

            using (var context = _contextFactory.CreateDbContext())
            {

                var totalCount = await context.Contents.Where(e => e.Subject.ToLower().Equals(subjectStr.ToLower())).CountAsync();

                var randomIndex = new Random().Next(0, totalCount);

                var randomContent = await context.Contents
                    .Where(e => e.Subject.ToLower().Equals(subjectStr.ToLower()))
                    .Skip(randomIndex)
                    .FirstOrDefaultAsync();

                return randomContent;
            }
        }

        internal async Task<Content?> UpdateContent(Content content)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var existingContent = await context.Contents.FindAsync(content.Id);
                if (existingContent == null)
                {
                    return null;
                }

                existingContent.Subject = content.Subject;
                existingContent.Theme = content.Theme;
                existingContent.Topic = content.Topic;
                existingContent.Subtopic = content.Subtopic;

                context.Contents.Update(existingContent);
                await context.SaveChangesAsync();

                return existingContent;
            }
        }

    }
}
