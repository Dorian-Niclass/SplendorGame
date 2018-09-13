using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Splendor
{
    /// <summary>
    /// contains methods and attributes to connect and deal with the database
    /// TO DO : le modèle de données n'est pas super, à revoir!!!!
    /// </summary>
    class ConnectionDB
    {
        //connection to the database
        private SQLiteConnection m_dbConnection; 

        /// <summary>
        /// constructor : creates the connection to the database SQLite
        /// </summary>
        public ConnectionDB()
        {
            LoadDB();
        }

        /// <summary>
        /// Create and insert data in the SQLite DB
        /// </summary>
        public void LoadDB()
        {
            if (!File.Exists("Splendor.sqlite"))
            {
                SQLiteConnection.CreateFile("Splendor.sqlite");

                m_dbConnection = new SQLiteConnection("Data Source=Splendor.sqlite;Version=3;");
                m_dbConnection.Open();
                 
                //create and insert players
                CreateInsertPlayer();
                //Create and insert cards
                //TO DO
                CreateInsertCards();
                //Create and insert ressources
                //TO DO
                CreateInsertRessources();
            }
        }

        /// <summary>
        /// get the list of cards according to the level
        /// </summary>
        /// <returns>cards stack</returns>
        public Stack<Card> GetListCardAccordingToLevel(int level)
        {
            //Get all the data from card table selecting them according to the data
            //TO DO
            //Create an object "Stack of Card"
            Stack<Card> listCard = new Stack<Card>();
            //do while to go to every record of the card table
            //while (....)
            //{
                //Get the ressourceid and the number of prestige points
                //Create a card object
                
                //select the cost of the card : look at the cost table (and other)
                
                //do while to go to every record of the card table
                //while (....)
                //{
                    //get the nbRessource of the cost
                //}
                //push card into the stack
                
            //}
            return listCard;
        }


        /// <summary>
        /// create the "player" table and insert data
        /// </summary>
        private void CreateInsertPlayer()
        {
            string sql = "CREATE TABLE player (id INT PRIMARY KEY, pseudo VARCHAR(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into player (id, pseudo) values (0, 'Fred')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into player (id, pseudo) values (1, 'Harry')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into player (id, pseudo) values (2, 'Sam')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        
        /// <summary>
        /// get the name of the player according to his id
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns></returns>
        public string GetPlayerName(int id)
        {
            string sql = "select pseudo from player where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader["pseudo"].ToString();
            }
            return name;
        }

        /// <summary>
        /// create the table "ressources" and insert data
        /// </summary>
        private void CreateInsertRessources()
        {
            string sql = "CREATE TABLE Ressources (idRessource INTEGER PRIMARY KEY, name VARCHAR(45))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "INSERT INTO Ressources VALUES(0, 'Rubis')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "INSERT INTO Ressources VALUES(1, 'Emeraude')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "INSERT INTO Ressources VALUES(2, 'Onyx')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "INSERT INTO Ressources VALUES(3, 'Saphir')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "INSERT INTO Ressources VALUES(4, 'Diamant')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }

        /// <summary>
        ///  create tables "cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        {
            string sql = "CREATE TABLE CardCost (idCost INTEGER PRIMARY KEY Autoincrement, fkCard INT, fkRessource INT, nbRessource INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE Cards (idCard INTEGER PRIMARY KEY Autoincrement, fkRessource INT, level INT, nbPtPrestige INT)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            using (var reader = new StreamReader(@".\Splendor_Cartes.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    int level = values[0] == "" ? 0 : Int32.Parse(values[0]);
                    int ress = values[1] == "" ? 0 : Int32.Parse(values[1]);
                    int prestige = values[2] == "" ? 0 : Int32.Parse(values[2]);
                    int rubis = values[3] == "" ? 0 : Int32.Parse(values[3]);
                    int emeraude = values[4] == "" ? 0 : Int32.Parse(values[4]);
                    int onyx = values[5] == "" ? 0 : Int32.Parse(values[5]);
                    int saphir = values[6] == "" ? 0 : Int32.Parse(values[6]);
                    int diamant = values[7] == "" ? 0 : Int32.Parse(values[7]);

                    int[] cost = { rubis, emeraude, onyx, saphir, diamant };

                    AddCard(ress, level, prestige, cost);
                }
            }

        }

        /// <summary>
        /// Add a card in the database with specific values
        /// </summary>
        /// <param name="ressource"></param>
        /// <param name="level"></param>
        /// <param name="prestige"></param>
        /// <param name="cost"></param>
        private void AddCard(int ressource, int level, int prestige, int[] cost)
        {
            string sql = "INSERT INTO Cards VALUES(NULL, "+ressource+", "+level+", "+prestige+")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "SELECT last_insert_rowid() AS id";
            command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = reader.GetInt32(0);

            for(int i = 0; i<cost.Count(); i++)
            {
                sql = "INSERT INTO CardCost VALUES(NULL, " + id + ", " + i + ", "+cost[i]+")";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
           
        }

    }
}
