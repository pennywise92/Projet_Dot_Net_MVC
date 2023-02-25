using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AchatEnLigne.Models;

namespace AchatEnLigne.Data
{
    public class AchatEnLigneContext : DbContext
    {
        public AchatEnLigneContext (DbContextOptions<AchatEnLigneContext> options)
            : base(options)
        {
        }

        public DbSet<AchatEnLigne.Models.LignePanierCommande> LignePanierCommande { get; set; }

        public DbSet<AchatEnLigne.Models.Panier> Panier { get; set; }

        public DbSet<AchatEnLigne.Models.Produit> Produit { get; set; }

        public DbSet<AchatEnLigne.Models.User> User { get; set; }

        public DbSet<AchatEnLigne.Models.Commande> Commande { get; set; }
    }
}
