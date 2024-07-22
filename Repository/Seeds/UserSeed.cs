using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Seeds
{
    internal class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    Name = "Cengizhan",
                    Surname = "Yavuz",
                    Age = 25,
                    IsActive = true,
                    CreatedDate = DateTime.Now


                },
                new User
                {
                    Id = 2,
                    Name = "John",
                    Surname = "Doe",
                    Age = 29,
                    IsActive = false,
                    CreatedDate = DateTime.Now
                });
        }
    }
}
