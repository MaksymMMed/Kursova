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
    public class SentenceConfig : IEntityTypeConfiguration<Sentence>
    {
        public void Configure(EntityTypeBuilder<Sentence> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasMany(x => x.Attemps)
                .WithOne(x => x.Sentence);
        }
    }
}
