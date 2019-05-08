using deSPICYtoINVADER.Characters;
using System.Diagnostics;

namespace deSPICYtoINVADER
{
    public abstract class Character
    {
        /// <summary>
        /// Propriétés pour avoir le nombre de vie
        /// </summary>
        public int Life { get; set; }
        /// <summary>
        /// Propriétés pour savoir si on doit supprimer le character
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
        /// Set GonnaDelete a true
        /// </summary>
        public void Death()
        {
            GonnaDelete = true;
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
            Debug.WriteLine("Character : Update()");
        }

        public abstract void Update();
        public abstract void GetShot(Bullet bull);
        protected abstract void Move(int direction);
        protected abstract void Shoot();
    }
}
