// ETML
// Auteur: friedlijo & andradebfi
// Date: 30.01.2019
// Description: Classe Menu du Spicy'Nvader
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Spicy_Nvader
{
    public class Swarm
    {
        private int _ennemyNbrLine;
        private int _ennemyNbrColumn;
        private int _swarmDirection;
        private List<Enemy> _swarm = new List<Enemy>();


        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="swarmEnnemy"></param>
        /// <param name="ennemyNumber"></param>
        /// <param name="swarmDirection"></param>
        public Swarm(int ennemyNbrLine, int ennemyNbrColumn, int swarmDirection)
        {
            _ennemyNbrLine = ennemyNbrLine;
            _ennemyNbrColumn = ennemyNbrColumn;
            _swarmDirection = swarmDirection;

        }

        /// <summary>
        /// Déplacement de l'essaim
        /// </summary>
        public void SwarmDirection()
        {

        }

        /// <summary>
        /// Création de l'essaim d'ennemis
        /// </summary>
        public void CreateSwarm()
        {
            
        }
    }
}