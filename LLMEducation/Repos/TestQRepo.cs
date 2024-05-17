using LLMEducation.Data;
using LLMEducation.Data.Entity;
using LLMEducation.Data.UIModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace LLMEducation.Repos
{
    public class SurveyDataRepo
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public SurveyDataRepo(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task SaveUISurvay(UIRating uiRating)
        {
            SurveyData surveyData = new SurveyData();
            surveyData.UserID = uiRating.User;

            surveyData.LLamaSurvey1 = uiRating.LammaSurvey.Rating1;
            surveyData.LLamaSurvey2 = uiRating.LammaSurvey.Rating2;
            surveyData.LLamaSurvey3 = uiRating.LammaSurvey.Rating3;
            surveyData.LLamaSurvey4 = uiRating.LammaSurvey.Rating4;

            surveyData.GemmaSurvey1 = uiRating.GemmaSurvey.Rating1;
            surveyData.GemmaSurvey2 = uiRating.GemmaSurvey.Rating2;
            surveyData.GemmaSurvey3 = uiRating.GemmaSurvey.Rating3;
            surveyData.GemmaSurvey4 = uiRating.GemmaSurvey.Rating4;

            surveyData.MistralSurvey1 = uiRating.MistralSurvey.Rating1;
            surveyData.MistralSurvey2 = uiRating.MistralSurvey.Rating2;
            surveyData.MistralSurvey3 = uiRating.MistralSurvey.Rating3;
            surveyData.MistralSurvey4 = uiRating.MistralSurvey.Rating4;

            surveyData.LastSurvey1 = uiRating.LastSurvey.Rating1;
            surveyData.LastSurvey2 = uiRating.LastSurvey.Rating2;
            surveyData.LastSurvey3 = uiRating.LastSurvey.Rating3;
            surveyData.LastSurvey4 = uiRating.LastSurvey.Rating4;

            surveyData.TestQMistralId = uiRating.MistralQuiz.testQ.Id;
            surveyData.TestQGemmaId = uiRating.GemmaQuiz.testQ.Id;
            surveyData.TestLlamaId = uiRating.LlamaQuiz.testQ.Id;

            await Save(surveyData);
        }

        public async Task Save(SurveyData data)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                await context.SurveyData.AddAsync(data);
                context.SaveChanges();
            }
        }
    }
}
