using Course_project.Controllers;
using Course_project.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project.Tests.Mocks
{
    internal static class FakeData
    {
        public static List<IdentityUser> Users
        {
            get
            {
                return new List<IdentityUser>
                {
                    new IdentityUser()
                    {
                        UserName= "Test 1",
                        Id = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                    },
                    new IdentityUser()
                    {
                        UserName= "Test 2",
                        Id = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203").ToString(),
                    },
                    new IdentityUser()
                    {
                        UserName = "Test 3",
                        Id = "user without collections"
                    },
                };
            }
        }


        public static List<Collection> Collections
        {
            get
            {
                return new List<Collection>
                {
                    new Collection()
                    {
                        Id = 1,
                        Name = "Collection 1",
                        AuthorId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                        Description = "Description 1",
                        Type = Models.Enums.CollectionType.Books,
                    },
                    new Collection()
                    {
                        Id = 2,
                        Name = "Collection 2",
                        AuthorId = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203").ToString(),
                        Description = "Description 2",
                        Type = Models.Enums.CollectionType.Books,
                    },
                };
            }
        }


        public static List<Item> Items
        {
            get
            {
                return new List<Item>
                {
                    new Item()
                    {
                        Id = 1,
                        Name = "Item 1",
                        CollectionId = 1,
                        AuthorId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                    },
                    new Item()
                    {
                        Id = 2,
                        Name = "Item 2",
                        CollectionId = 2,
                        AuthorId = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203").ToString(),
                    },
                };
            }
        }


        public static List<Tag> Tags
        {
            get
            {
                return new List<Tag>
                {
                    new Tag()
                    {
                        Id = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                        Name = "tag 1",
                    },
                    new Tag()
                    {
                        Name = "tag 2",
                        Id = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203").ToString(),
                    },
                };
            }
        }

        public static List<TagConnection> TagConnections
        {
            get
            {
                return new List<TagConnection>
                {
                    new TagConnection()
                    {
                        Id = 1,
                        TagId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                        ItemId = 1,
                    },
                    new TagConnection()
                    {
                        Id = 2,
                        TagId = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203").ToString(),
                        ItemId= 2,
                    },
                };
            }
        }
    }
}
