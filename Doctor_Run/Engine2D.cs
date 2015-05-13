using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Doctor_Run;

namespace Doctor_run
{
    /*
     * Cette classe s'occupe de gérer tous les problèmes liés à un environnement 2D
     */
    class Engine2D
    {
        // Positions relatives
        public const int CROISEMENT = -1; // Si les 2 objets se croisent
        public const int EN_DESSOUS = 0;
        public const int AU_DESSUS = 1;
        public const int A_DROITE = 2;
        public const int A_GAUCHE = 3;

        // Elements de l'environnement
        public const int MUR_BAS = 0;
        public const int MUR_HAUT = 1;

        // les parties gauche et droite sont considérées comme des "murs" même si la balle les traverse
        public const int MUR_DROIT = 2;
        public const int MUR_GAUCHE = 3;

        public static Boolean testCollision(Object obj1, BoundingBox obj2)
        {
            // On réalise le mvt dans une bounding box de test et on renvoie le résultat de l'intersection avec l'objet obj
            Vector3 upperLeftCorner = new Vector3(0, 0, 0);
            Vector3 bottomRightCorner = new Vector3(0, 0, 0);

            if (obj1 is Doctor)
            {
                Doctor who = (Doctor) obj1;
                upperLeftCorner.X = who.Position.X + who.Velocity.X;
                upperLeftCorner.Y = who.Position.Y + who.Velocity.Y;
                bottomRightCorner.X = who.Position.X + who.FrameSize.X + who.Velocity.X;
                bottomRightCorner.Y = who.Position.Y + who.FrameSize.Y + who.Velocity.Y;
            }

            if (obj1 is Foe)
            {
                Foe killer = (Foe)obj1;
                upperLeftCorner.X = killer.Position.X + killer.Velocity.X;
                upperLeftCorner.Y = killer.Position.Y + killer.Velocity.Y;
                bottomRightCorner.X = killer.Position.X + killer.FrameSize.X + killer.Velocity.X;
                bottomRightCorner.Y = killer.Position.Y + killer.FrameSize.Y + killer.Velocity.Y;
            }

            BoundingBox bbox_test = new BoundingBox(upperLeftCorner, bottomRightCorner);

            return bbox_test.Intersects(obj2);
        }

        /// <summary>
        /// Donne la position d'un objet par rapport à un autre
        /// </summary>
        /// <param name='obj1'>
        ///     int[] - un tableau contenant des informations sur l'objet à localiser :
        ///     position en X, position en Y, largeur, hauteur
        /// </param>
        /// <param name='obj2'>
        ///     int[] - un tableau de nombres décimaux contenant des informations sur l'objet de référence dans 
        ///     cet ordre :
        ///     position en X, position en Y, largeur, hauteur
        /// </param>
        /// <returns>
        ///     Retourne un vecteur contenant la comparaison sur l'axe des x et celle sur l'axe des y
        ///     Les positions sont représentées par les constantes définies dans MoteurJeu suivantes :
        ///       - EN_DESSOUS
        ///       - AU_DESSOUS
        ///       - A_DROITE
        ///       - A_GAUCHE
        /// </returns>
        public static int[] getRelativePosition(float[] obj, float[] reference)
        {
            int[] positionRel = { -1, -1 };


            // axe des X
            if (obj[0] >= reference[0] + reference[2])
                positionRel[0] = A_DROITE;
            else if (obj[0] + obj[2] <= reference[0])
                positionRel[0] = A_GAUCHE;

            // axe des Y
            if (obj[1] >= reference[1] + reference[3])
                positionRel[1] = EN_DESSOUS;
            else if (obj[1] + obj[3] <= reference[1])
                positionRel[1] = AU_DESSUS;

            return positionRel;
        }
    }
}
