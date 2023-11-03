
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configuration;
public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("team");

        builder.HasIndex(e => e.Name, "idx_team_name").IsUnique();

        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");


        /* builder.HasMany(d => d.Drivers)
                .WithMany(p => p.Teams)
                .UsingEntity<Teamdriver>(
                    
                    r => r.HasOne<Driver>().WithMany()
                        .HasForeignKey("IdDriver")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IdDriver"),
                    l => l.HasOne<Team>().WithMany()
                        .HasForeignKey("IdTeam")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IdTeam"),
                    j =>
                    {
                        j.HasKey("IdTeam", "IdDriver")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("teamdriver");
                        j.HasIndex(new[] { "IdDriver" }, "IdDriver_idx");
                    }); */
        builder
        .HasMany(p => p.Drivers)
        .WithMany(p => p.Teams)
        .UsingEntity<Teamdriver>(
          j => j
            .HasOne(pt => pt.Driver)
            .WithMany(t => t.Teamdrivers)
            .HasForeignKey(pt => pt.IdDriver),
          j => j
            .HasOne(pt => pt.Team)
            .WithMany(t => t.Teamdrivers)
            .HasForeignKey(pt => pt.Idteam),
          j => 
            {
              j.HasKey(t => new {t.IdDriver, t.Idteam});
              j.ToTable("teamdriver");
            });
    }
}
