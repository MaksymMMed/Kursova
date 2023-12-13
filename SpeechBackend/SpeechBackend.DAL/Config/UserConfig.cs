using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpeechBackend.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(50);

            builder
                .Property(x => x.Surname)
                .HasMaxLength(50);

            builder
                .Property(x => x.Email)
                .HasMaxLength(50);

            builder
                .HasMany(x => x.Attemps)
                .WithOne(x => x.User);

            builder
                .Property (x => x.Role)
                .HasMaxLength(10);
        }
    }
}
