using OnlineTesty_Library.Contexts;
using OnlineTesty_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Repositories
{
    public class ExamRepositories : BaseRepositoryEF, IExamRepositories
    {
        public ExamRepositories(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void AddQuestionToExam(ExamQuestion question)
        {
            throw new NotImplementedException();
        }

        public Guid Create(Exam model)
        {
            this.GetDbSet<Exam>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public void Delete(Guid? ID)
        {
            var remove = Read(ID);
            this.GetDbSet<Exam>().Remove(remove);
            this.UnitOfWork.SaveChanges();
        }

        public IEnumerable<Exam> FindAll()
        {
            return this.GetDbSet<Exam>();
        }

        public Exam Read(Guid? ID)
        {
            return this.GetDbSet<Exam>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public void Update(Exam model)
        {
            throw new NotImplementedException();
        }
    }

    public interface IExamRepositories
    {
        Exam Read(Guid? ID);
        Guid Create(Exam model);
        void Delete(Guid? ID);
        void Update(Exam model);
        void AddQuestionToExam(ExamQuestion question);
        IEnumerable<Exam> FindAll();
    }
}
