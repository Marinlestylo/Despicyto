///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe asbtract pour créer la classe player et la classe enemy
using deSPICYtoINVADER.Utils;

namespace deSPICYtoINVADER.Characters
{
    /// <summary>
    /// Classe character dont vont hériter les classes Player et Enemy
    /// </summary>
    public abstract class Character
    {
        /// <summary>
        /// Propriétés pour avoir le nombre de vie
        /// </summary>
        public int Life { get; protected set; }
        /// <summary>
        /// Propriétés pour savoir si on doit supprimer le character, si les vies arrivent à 0, on set à true;
        /// </summary>
        public bool GonnaDelete { get; protected set; }

        /* Attributs */
        protected Point _position;//Coord X et Coord Y du character
        protected int _direction;//Sens dans lequel le character va
        protected string[] _design;//Tableau de string pour le design du character

        /// <summary>
        /// Constructeur de character
        /// </summary>
        /// <param name="lives">Nombre de vie</param>
        /// <param name="position">La position (toujours tout en haut du character et centré en largeur)</param>
        public Character(int lives, Point position)
        {
            Life = lives;
            _position = position;
            GonnaDelete = false;
        }

        /// <summary>
        /// Dessine le character, peut importe le design
        /// </summary>
        protected void Draw()
        {
            for (int i = 0; i < _design.Length; i++)
            {
                for (int j = 0; j < _design[i].Length; j++)
                {
                    Game.allChars[_position.Y + i][_position.X - _design[i].Length / 2 + j] = _design[i][j];
                }
            }
        }

        /// <summary>
        /// Appelle la méthode Draw() (Cette méthode devra être réécrit pour faire des trucs en plus)
        /// </summary>
        public virtual void BaseUpdate()
        {
            Draw();
        }

        /// <summary>
        /// Tous les tics, cette methode va update le player ou l'enemy (la position, la hitbox, les tirs, etc)
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Pour voir si on se fait shooter par la bullet passée en paramètre
        /// </summary>
        /// <param name="bull"></param>
        public abstract void GetShot(Bullet bull);

        /// <summary>
        /// Permet de bouger et d'afficher le sprites du characters qui bouge
        /// </summary>
        /// <param name="direction"></param>
        protected abstract void Move(int direction);

        /// <summary>
        /// Permet de tirer une bullet
        /// </summary>
        protected abstract void Shoot();
    }
}
