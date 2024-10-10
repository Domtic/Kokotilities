
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Koko
{
    public static class Utilities
    {
        public static double GetRandomDouble(double min, double max)
        {
            System.Random random = new System.Random();
            return random.NextDouble() * (max - min) + min;
        }
        //Returns Enum elemtns length
        public static int GetEnumLength<T>() where T : System.Enum
        {
            return System.Enum.GetValues(typeof(T)).Length;
        }

        // Is Mouse over a UI Element? Used for ignoring World clicks through UI
        public static bool IsPointerOverUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
               /* PointerEventData pe = new PointerEventData(EventSystem.current);
                pe.position = Input.mousePosition;
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pe, hits);
                foreach(RaycastResult hit in hits)
                {
                    Debug.Log(hit.gameObject.name);
                }*/
                return true;
            }
            else
            {

                PointerEventData pe = new PointerEventData(EventSystem.current);
                pe.position = Input.mousePosition;
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pe, hits);
                return hits.Count > 0;
            }
        }


        /// <summary>
        /// Returns true if the gameobject is part of the layer sent
        /// </summary>
        /// <param name="_ObjectLayer"></param>
        /// <returns></returns>
        public static bool IsPointerOverGameObject(LayerMask _ObjectLayer)
        {
            PointerEventData pe = new PointerEventData(EventSystem.current);
            pe.position = Input.mousePosition;
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pe, hits);
            bool IsUIInthemiddle = false;
            bool ObjectInLayerFound = false;
            foreach (RaycastResult hit in hits)
            {
                //Debug.Log(hit.gameObject.name + "-*" + hit.gameObject.layer + "-" + _ObjectLayer.value);
                //Default UI layer is 5
                if (hit.gameObject.layer == 5)
                {
                    IsUIInthemiddle = true;
                }
                   
                if ((1 << hit.gameObject.layer) == _ObjectLayer.value)
                {
                    //Debug.Log(hit.gameObject.name);
                    ObjectInLayerFound = true;
                }

            }

            if (IsUIInthemiddle)
                return false;
            else if (ObjectInLayerFound)
                return true;
            else
                return false;

        }
        //Returns random color
        public static Color GetRandomColor()
        {
            return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        }


        // Get a random male name and optionally single letter surname
        public static string GetRandomName(bool withSurname = false)
        {
            List<string> firstNameList = new List<string>(){
                   "Aaron", "Adam", "Adrian", "Aidan", "Alan", "Albert", "Alexander", "Alfred", "Andrew", "Anthony",
                    "Arthur", "Austin", "Benjamin", "Blake", "Brandon", "Brian", "Bruce", "Bryan", "Caleb", "Cameron",
                    "Carl", "Charles", "Christian", "Christopher", "Clark", "Cole", "Colin", "Connor", "Craig", "Daniel",
                    "David", "Dennis", "Derek", "Dominic", "Donald", "Douglas", "Dylan", "Edward", "Eli", "Elijah",
                    "Elliot", "Ethan", "Evan", "Felix", "Francis", "Frank", "Gabriel", "Gavin", "Geoffrey", "George",
                    "Gordon", "Graham", "Grant", "Gregory", "Harold", "Harry", "Henry", "Howard", "Hudson", "Hunter",
                    "Ian", "Isaac", "Jack", "Jackson", "Jacob", "James", "Jason", "Jeffrey", "Jeremy", "Jesse",
                    "John", "Jonathan", "Joseph", "Joshua", "Julian", "Justin", "Keith", "Kevin", "Kyle", "Landon",
                    "Lawrence", "Leonard", "Liam", "Logan", "Louis", "Lucas", "Mark", "Martin", "Mason", "Matthew",
                    "Michael", "Nathan", "Nathaniel", "Neil", "Nicholas", "Noah", "Norman", "Oliver", "Oscar", "Owen",
                    "Patrick", "Paul", "Peter", "Philip", "Quentin", "Raymond", "Richard", "Robert", "Roger", "Ronald",
                    "Russell", "Ryan", "Samuel", "Scott", "Sean", "Sebastian", "Simon", "Spencer", "Stephen", "Steven",
                    "Stuart", "Theodore", "Thomas", "Timothy", "Travis", "Trevor", "Tyler", "Victor", "Vincent", "Walter",
                    "Wayne", "William", "Zachary",
                     "Víctor", "Álvaro", "Beto", "Diego", "Francisco", "Guadalupe", "Héctor", "Iván", "José", "Miguel",
                     "Pablo", "Rafael", "Ulises", "Walter", "Xavier", "Yair", "Zacarías", "Arturo", "David", "Esteban",
                     "Felipe", "Ignacio", "Jorge", "Manuel", "Nicolás", "Pedro", "Ramón", "Wilfredo", "Alberto", "Domingo",
                     "Emilio", "Federico", "Gonzalo", "Iván", "Juan", "Marcos", "Nuria", "Omar", "Patricio", "Roberto",
                     "Teodoro", "Vicente", "Álvaro", "Bernardo", "Camilo", "Daniel", "Fabián", "Gregorio", "Julio",
                     "Martín", "Orlando", "Ramiro", "Ricardo", "Santiago", "Sebastián", "Xavi", "Amadeo", "Bernardo",
                     "César", "Esteban", "Florencio", "Guillermo", "Horacio", "Isidro", "Jaime", "Mario", "Nicolás",
                     "Osvaldo", "Ramiro", "Sebastián", "Ulises", "Valentín", "Xenon", "Álvaro", "Benjamín", "Cristóbal",
                     "Domitilo", "Ezequiel", "Fausto", "Gerardo", "Héctor", "Isaac", "Jonathan", "León", "Manuel",
                     "Natalio", "Pedro", "Rafael", "Salvador", "Thiago", "Valentín", "Walter", "Xerxes", "Yago", "Zenón",
                     "Andrés", "Boris", "Cristiano", "Enrique", "Fermín", "Genaro", "Hermes", "Jacinto", "Julio",
                     "Leandro", "Marcos", "Octavio", "Ramón", "Rubén", "Simón", "Tristán", "Vladimir", "Yahir", "Zacarías",
                     "Antonio", "Bautista", "Claudio", "Eduardo", "Faustino", "Genaro", "Hilario", "Jacobo", "Lázaro",
                     "Mauricio", "Néstor", "Pascual", "Ricardo", "Saúl", "Tobías", "Ulises", "Vicente", "Ximeno", "Zacarías",
                     "Alfonso", "Bernardo", "Ciro", "Efraín", "Florencio", "Gonzalo", "Homero", "Jeremías", "Lucas",
                     "Matías", "Noé", "Pablo", "Rodrigo", "Samuel", "Tadeo", "Valerio", "Xerxes", "Yago", "Zenón",
                     "Arturo", "Braulio", "Cristian", "Esteban", "Felipe", "Germán", "Hugo", "Jesús", "Lorenzo",
                     "Maximiliano", "Norberto", "Pedro", "Ramiro", "Sergio", "Timoteo", "Valentín", "Ximeno", "Yago", "Zacarías",
                     "Alejo", "Bruno", "Clemente", "Emilio", "Fernando", "Guillermo", "Héctor", "Joaquín", "Manuel",
                     "Miguel", "Nicolás", "Pascual", "Roberto", "Tomás", "Vicente", "Xenón", "Yadir", "Zenón",
                     "Ángel", "Bartolomé", "Cristóbal", "Esteban", "Federico", "Germán", "Héctor", "Julián", "Lucas",
                     "Maximiliano", "Noé", "Pablo", "Raúl", "Simón", "Tobías", "Valerio", "Ximeno", "Yago", "Zenón"}; 

            if (!withSurname)
            {
                return firstNameList[UnityEngine.Random.Range(0, firstNameList.Count)];
            }
            else
            {
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYWZ";
                return firstNameList[UnityEngine.Random.Range(0, firstNameList.Count)] + " " + alphabet[UnityEngine.Random.Range(0, alphabet.Length)] + ".";
            }
        }

        public static bool IsColorSimilar(Color colorA, Color colorB, float maxDiff)
        {
            float rDiff = Mathf.Abs(colorA.r - colorB.r);
            float gDiff = Mathf.Abs(colorA.g - colorB.g);
            float bDiff = Mathf.Abs(colorA.b - colorB.b);
            float aDiff = Mathf.Abs(colorA.a - colorB.a);

            float totalDiff = rDiff + gDiff + bDiff + aDiff;
            return totalDiff < maxDiff;
        }


        public static float GetColorDifference(Color colorA, Color colorB)
        {
            float rDiff = Mathf.Abs(colorA.r - colorB.r);
            float gDiff = Mathf.Abs(colorA.g - colorB.g);
            float bDiff = Mathf.Abs(colorA.b - colorB.b);
            float aDiff = Mathf.Abs(colorA.a - colorB.a);

            float totalDiff = rDiff + gDiff + bDiff + aDiff;
            return totalDiff;
        }



        // Get UI Position from World Position
        public static Vector2 GetWorldUIPosition(Vector3 worldPosition, Transform parent, Camera uiCamera, Camera worldCamera)
        {
            Vector3 screenPosition = worldCamera.WorldToScreenPoint(worldPosition);
            Vector3 uiCameraWorldPosition = uiCamera.ScreenToWorldPoint(screenPosition);
            Vector3 localPos = parent.InverseTransformPoint(uiCameraWorldPosition);
            return new Vector2(localPos.x, localPos.y);
        }

        public static Vector3 GetWorldPositionFromUIZeroZ()
        {
            Vector3 vec = GetWorldPositionFromUI(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        // Get World Position from UI Position
        public static Vector3 GetWorldPositionFromUI()
        {
            return GetWorldPositionFromUI(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetWorldPositionFromUI(Camera worldCamera)
        {
            return GetWorldPositionFromUI(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetWorldPositionFromUI(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public static Vector3 GetWorldPositionFromUI_Perspective()
        {
            return GetWorldPositionFromUI_Perspective(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetWorldPositionFromUI_Perspective(Camera worldCamera)
        {
            return GetWorldPositionFromUI_Perspective(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetWorldPositionFromUI_Perspective(Vector3 screenPosition, Camera worldCamera)
        {
            Ray ray = worldCamera.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0f));
            float distance;
            xy.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }

        public static string ConvertSecondsToMinutes(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            return $"{minutes}:{remainingSeconds:D2}";
        }
    }
  
}
