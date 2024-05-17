using DocumentFormat.OpenXml.Presentation;
using LLMEducation.Data.Entity;
using LLMEducation.Data.Migrations;
using LLMEducation.Data.UIModels;
using LLMEducation.Repos;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

namespace LLMEducation.Service
{
    public class ContentService
    {

        public readonly ContentRepo contentRepo;
        public ContentService(ContentRepo _contentRepo)
        {
            contentRepo = _contentRepo;
        }

        public async Task<List<Content>> GetAllContents()
        {

            return await contentRepo.GetAllContent();
        }
        public async Task<Content?> GetById(string id)
        {
            try
            {
                return await contentRepo.GetContent(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Content?> Update(Content content)
        {
            try
            {
                return await contentRepo.UpdateContent(content);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Content?> Save(Content content)
        {
            try
            {
                return await contentRepo.SaveContent(content);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task Delete(Content content)
        {
            try
            {
                await contentRepo.DeleteContent(content);

            }
            catch (Exception ex)
            {
            }
        }

    }

}
