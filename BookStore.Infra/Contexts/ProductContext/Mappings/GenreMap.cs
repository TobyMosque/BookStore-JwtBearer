﻿using BookStore.Core.Contexts.ProductContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infra.Contexts.ProductContext.Mappings;

public class GenreMap : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(genre => genre.Id);

        builder.Property(genre => genre.GenreName).HasColumnName("Name").HasColumnType("NVARCHAR").HasMaxLength(40).IsRequired(true);
    }
}
