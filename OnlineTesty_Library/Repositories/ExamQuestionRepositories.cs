using OnlineTesty_Library.Contexts;
using OnlineTesty_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Repositories
{
    public class ExamQuestionsRepositories : BaseRepositoryEF, IExamQuestionsRepositories
    {
        private readonly IExamRepositories _examRepositories;

        public ExamQuestionsRepositories(IUnitOfWork unitOfWork, IExamRepositories examRepositories) : base(unitOfWork)
        {
            _examRepositories = examRepositories;
        }

        public Guid Create(ExamQuestion model)
        {
            this.GetDbSet<ExamQuestion>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public void Delete(Guid? ID)
        {
            var remove = Read(ID);
            this.GetDbSet<ExamQuestion>().Remove(remove);
            this.UnitOfWork.SaveChanges();
        }

        public IEnumerable<ExamQuestion> FindAll()
        {
            return this.GetDbSet<ExamQuestion>();
        }

        public Guid? GetQuestionId(string Question)
        {
            var getId = ReadByQuestion(Question);
            return getId.ID;
        }

        public ExamQuestion Read(Guid? ID)
        {
            return this.GetDbSet<ExamQuestion>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public ExamQuestion ReadByQuestion(string Question)
        {
            return this.GetDbSet<ExamQuestion>()
                .Where(e => e.Question == Question).FirstOrDefault();
        }

        public IEnumerable<ExamQuestion> ReadAllQuestionsByExam(Guid examId)
        {
            string examIdToUpper = examId.ToString().ToUpper();
            IQueryable<ExamQuestion> allExamQuestionsByExamId = Enumerable.Empty<ExamQuestion>().AsQueryable();
            allExamQuestionsByExamId = GetDbSet<ExamQuestion>().Where(e => e.ExamID.ToString() == examIdToUpper);
            return allExamQuestionsByExamId;
        }

        public void Update(ExamQuestion model)
        {
            var update = Read(model.ID);
            //update.Name = model.Name; TODO
            this.UnitOfWork.SaveChanges();
        }

        public void Delete(Guid ID)
        {
            throw new NotImplementedException();
        }
    }

    public interface IExamQuestionsRepositories
    {
        ExamQuestion Read(Guid? ID);
        Guid Create(ExamQuestion model);
        Guid? GetQuestionId(string Question);
        void Update(ExamQuestion model);
        void Delete(Guid ID);
        IEnumerable<ExamQuestion> FindAll();
        IEnumerable<ExamQuestion> ReadAllQuestionsByExam(Guid id);
    }
}
