using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteTecnicoIdealSoft.API.Entities;

namespace TesteTecnicoIdealSoft.API.Data.EntitiesMapping;

public sealed class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Nome).HasColumnType("varchar(50)")
            .HasColumnName("nome").IsRequired(true);

        builder.Property(p => p.Sobrenome).HasColumnType("varchar(100)")
            .HasColumnName("sobrenome").IsRequired(true);

        builder.Property(p => p.Telefone).HasColumnType("char(11)")
            .HasColumnName("telefone").IsRequired(true);
    }
}
