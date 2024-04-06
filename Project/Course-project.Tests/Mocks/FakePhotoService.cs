using Course_project.Data;
using Course_project.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project.Tests.Mocks
{
    public class FakePhotoService
    {
        public Mock<IPhotoService> Mock;
        public IPhotoService PhotoService;


        public FakePhotoService()
        {
            Mock = new Mock<IPhotoService>();

            PhotoService = Mock.Object;
        }
    }
}
