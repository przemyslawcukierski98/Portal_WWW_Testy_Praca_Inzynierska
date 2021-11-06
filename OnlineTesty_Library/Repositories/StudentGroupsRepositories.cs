using OnlineTesty_Library.Contexts;
using OnlineTesty_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Repositories
{
    public class StudentGroupsRepositories : BaseRepositoryEF, IStudentGroupsRepositories
    {
        public StudentGroupsRepositories(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Guid Create(StudentGroup model)
        {
            this.GetDbSet<StudentGroup>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public void Delete(Guid? ID)
        {
            var remove = Read(ID);
            this.GetDbSet<StudentGroup>().Remove(remove);
            this.UnitOfWork.SaveChanges();
        }

        public IEnumerable<StudentGroup> FindBy(Expression<Func<StudentGroup, bool>> predicate)
        {
            return this.GetDbSet<StudentGroup>()
                .Where(predicate);
        }

        public StudentGroup Read(Guid? ID)
        {
            return this.GetDbSet<StudentGroup>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public void Update(StudentGroup model)
        {
            var update = Read(model.ID);
            update.Name = model.Name;
            this.UnitOfWork.SaveChanges();
        }

        public IEnumerable<StudentGroup> FindAll()
        {
            return this.GetDbSet<StudentGroup>();
        }
    }

    public interface IStudentGroupsRepositories
    {
        StudentGroup Read(Guid? ID);
        Guid Create(StudentGroup model);
        void Update(StudentGroup model);
        void Delete(Guid? ID);
        IEnumerable<StudentGroup> FindAll();
    }
}
