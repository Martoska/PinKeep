using Course_project.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project.Tests.Mocks
{
    internal class FakeDbContext
    {
        public Mock<ApplicationDbContext> Mock;
        public ApplicationDbContext DbContext;


        public FakeDbContext()
        {
            Mock = new Mock<ApplicationDbContext>();

            DbContext = Mock.Object;
        }
    }
}
