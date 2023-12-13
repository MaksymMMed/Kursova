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
    public class AttempConfig : IEntityTypeConfiguration<Attemp>
    {
        public void Configure(EntityTypeBuilder<Attemp> builder)
        {
            builder
                .HasKey(x => x.Id);
 
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Attemps);

            builder
                .HasOne(x => x.Sentence)
                .WithMany(x => x.Attemps);


        }
    }
}
