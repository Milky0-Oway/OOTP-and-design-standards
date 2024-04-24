using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;

namespace lab2
{
    public partial class Form1 : Form
    {
        private ComboBox animalComboBox;
        private ListBox animalListBox;
        private Button createButton;
        private ComboBox additionalActionsComboBox;
        private Button performActionButton;
        private Button serializeBButton;
        private Button deserializeBButton;
        private Button serializeJButton;
        private Button deserializeJButton;
        private Button editButton;
        private Button deleteButton;
        private Dictionary<string, Bitmap> images = new Dictionary<string, Bitmap>();
        private List<PictureBox> picturesBox= new List<PictureBox>();
        private Random random;
        private Dictionary<string, List<Control>> animalControls = new Dictionary<string, List<Control>>();
        private Dictionary<string, List<string>> animalFields = new Dictionary<string, List<string>>();
        private TextBox additionalTextBox;
        private AnimalProvider animalProvider = new AnimalProvider();
        private List<Animals> animals = new List<Animals>();
        private PluginManager pluginManager = new PluginManager();
        private List<Type> knownType = new List<Type>();

        public Form1()
        {
            InitializeComponent();
            
            random = new Random();
            images["Cat"] = Properties.Resources.cat1;
            images["Dog"] = Properties.Resources.dog1;
            images["Parrot"] = Properties.Resources.parrot1;
            images["Cow"] = Properties.Resources.cow;
            images["Penguin"] = Properties.Resources.penguin;
            
            additionalActionsComboBox = new ComboBox();
            additionalActionsComboBox.Location = new Point(110, 205);
            Controls.Add(additionalActionsComboBox);

            performActionButton = new Button();
            performActionButton.Text = "Perform";
            performActionButton.Location = new Point(130, 240);
            performActionButton.Click += PerformActionButton_Click;
            Controls.Add(performActionButton);
            
            serializeJButton = new Button();
            serializeJButton.Text = "JSON Serialize";
            serializeJButton.Location = new Point(130, 40);
            serializeJButton.Size = new Size(100, 25);
            serializeJButton.Click += buttonJSerialize_Click;
            Controls.Add(serializeJButton);
            
            deserializeJButton = new Button();
            deserializeJButton.Text = "JSON Deserialize";
            deserializeJButton.Location = new Point(130, 70);
            deserializeJButton.Size = new Size(100, 25);
            deserializeJButton.Click += buttonJDeserialize_Click;
            Controls.Add(deserializeJButton);
            
            Label additionalLabel = new Label();
            additionalLabel.Text = "Additional:";
            additionalTextBox = new TextBox();
            additionalLabel.Location = new Point(110, 150);
            additionalTextBox.Location = new Point(110, 175);
            Controls.Add(additionalLabel);
            Controls.Add(additionalTextBox);
            
            animalComboBox = new ComboBox();
            animalComboBox.Location = new Point(110, 5);
            animalComboBox.SelectedIndexChanged += animalComboBox_SelectedIndexChanged;
            Controls.Add(animalComboBox);

            animalListBox = new ListBox();
            animalListBox.Size = new Size(150, 140);
            animalListBox.Location = new Point(10, 300);
            Controls.Add(animalListBox);

            createButton = new Button();
            createButton.Text = "Create";
            createButton.Location = new Point(10, 240);
            createButton.Click += CreateButton_Click;
            Controls.Add(createButton);
            
            additionalActionsComboBox.Items.Add("Move");
            additionalActionsComboBox.Items.Add("Make sound");
            
            try
            {
                pluginManager.LoadPlugins(@"D:\Study\OOP\lab2\lab2\lab2\plugins");
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (Exception innerEx in ex.LoaderExceptions)
                {
                    Console.WriteLine(innerEx.Message);
                }
            }
            
            List<IAnimalPlugin> plugins = pluginManager.GetPlugins();
            foreach (IAnimalPlugin plugin in plugins)
            {
                string animalType = plugin.GetName();
                List<string> additionalFields = plugin.GetAdditionalFields();
                List<string> actions = plugin.GetAdditionalActions();
                
                animalControls[animalType] = new List<Control>();
                animalFields[animalType] = additionalFields;
                animalComboBox.Items.Add(plugin.GetName());

                foreach (var action in actions)
                {
                    additionalActionsComboBox.Items.Add(action);
                }
                Dictionary<string, string> additional = new Dictionary<string, string>();
                foreach (var action in additionalFields)
                {
                    additional.Add(action, "0");
                }
                Animals animal = plugin.CreateAnimal("0", 0, additional);
                knownType.Add(animal.GetType());
            }
        }

        private void animalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAnimal = animalComboBox.SelectedItem.ToString();
            CreateAnimalControls(selectedAnimal);
        }

        private void CreateAnimalControls(string animalType)
        {
            ClearAnimalControls();
            
            List<string> additionalFields = animalFields[animalType];

            Label nameLabel = new Label();
            nameLabel.Text = "Name:";
            TextBox nameTextBox = new TextBox();

            Label ageLabel = new Label();
            ageLabel.Text = "Age:";
            TextBox ageTextBox = new TextBox();

            nameLabel.Location = new Point(5, 5);
            nameTextBox.Location = new Point(5, 25);
            ageLabel.Location = new Point(5, 50);
            ageTextBox.Location = new Point(5, 70);

            List<Control> nameControls = new List<Control> { nameLabel, nameTextBox };
            animalControls["Name"] = nameControls;

            List<Control> ageControls = new List<Control> { ageLabel, ageTextBox };
            animalControls["Age"] = ageControls;

            Controls.AddRange(nameControls.ToArray());
            Controls.AddRange(ageControls.ToArray());

            if (additionalFields != null && additionalFields.Count > 0)
            {
                int y = 95;
                foreach (var field in additionalFields)
                {
                    Label fieldLabel = new Label();
                    fieldLabel.Text = field + ":";
                    TextBox fieldTextBox = new TextBox();
                    fieldLabel.Location = new Point(5, y);
                    fieldTextBox.Location = new Point(5, y + 20);
                    List<Control> fieldControls = new List<Control> { fieldLabel, fieldTextBox };
                    animalControls[field] = fieldControls;
                    Controls.AddRange(fieldControls.ToArray());
                    y += 50;
                }
            }
        }

        private void ClearAnimalControls()
        {
            foreach (var controls in animalControls.Values)
            {
                foreach (var control in controls)
                {
                    Controls.Remove(control);
                }
                controls.Clear();
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string selectedAnimal = animalComboBox.SelectedItem.ToString();
            Dictionary<string, string> additionalInfo = new Dictionary<string, string>();
            foreach (var controlPair in animalControls)
            {
                if (controlPair.Value.Count == 2)
                {
                    string value = ((TextBox)controlPair.Value[1]).Text;
                    additionalInfo.Add(controlPair.Key, value);
                }
            }
            string name = ((TextBox)animalControls["Name"][1]).Text;
            int age = int.Parse(((TextBox)animalControls["Age"][1]).Text);
            IAnimalPlugin animalPlugin = GetAnimalPlugin(selectedAnimal);
            Animals animal = animalPlugin.CreateAnimal(name, age, additionalInfo);
            if (animal != null)
            {                
                animalProvider[name] = animal;
                animalListBox.Items.Add(name);
                string selectedAnimalType = animal.GetType().Name;
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(random.Next(300, 700), random.Next(0, 400));
                pictureBox.Size = new Size(100, 100);
                pictureBox.Image = images[selectedAnimalType];
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.BackColor = Color.Transparent;
                picturesBox.Add(pictureBox);
                Controls.Add(pictureBox);
                pictureBox.BringToFront();
            }
        }
        
        private IAnimalPlugin GetAnimalPlugin(string animalType)
        {
            foreach (var plugin in pluginManager.GetPlugins())
            {
                if (plugin.GetName() == animalType)
                {
                    return plugin;
                }
            }
            throw new NotSupportedException($"Animal type '{animalType}' is not supported.");
        }

        private void PerformActionButton_Click(object sender, EventArgs eventArgs)
        {
            string selectedAction = additionalActionsComboBox.SelectedItem.ToString();
            string selectedAnimal = animalListBox.SelectedItem.ToString();

            if (selectedAction == "Move")
            {
                animalProvider[selectedAnimal].Move();
            }
            else if (selectedAction == "Make sound")
            {
                animalProvider[selectedAnimal].MakeSound();
            }
            else
            {
                string selectedAnimalType = animalProvider[selectedAnimal].GetType().Name;
                IAnimalPlugin plugin = GetAnimalPlugin(selectedAnimalType);
                string additional = additionalTextBox.Text;
                plugin.PerformAction(selectedAction, additional);
            }
        }
        
        private void buttonJSerialize_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    JSerializeAnimals(saveFileDialog.FileName);
                }
            }
        }

        private void buttonJDeserialize_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<Animals> deserializedAnimals = JDeserializeAnimals(openFileDialog.FileName);
                    if (deserializedAnimals != null)
                    {
                        animalListBox.Items.Clear();
                        foreach (var picture in picturesBox)
                        {
                            picture.Dispose();
                        }
                        foreach (Animals animal in deserializedAnimals)
                        {
                            if (animal != null && animal.Name != null)
                            {
                                animalListBox.Items.Add(animal.Name);
                                animalProvider[animal.Name] = animal;
                                string selectedAnimalType = animal.GetType().Name;
                                PictureBox pictureBox = new PictureBox();
                                pictureBox.Location = new Point(random.Next(300, 700), random.Next(0, 400));
                                pictureBox.Size = new Size(100, 100);
                                pictureBox.Image = images[selectedAnimalType];
                                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                pictureBox.BackColor = Color.Transparent;
                                picturesBox.Add(pictureBox);
                                Controls.Add(pictureBox);
                                pictureBox.BringToFront();
                            }
                        }
                        MessageBox.Show("Объекты успешно загружены из файла.");
                    }
                }
            }
        }
        
        private void JSerializeAnimals(string filePath)
        {
            try
            {
                animals.Clear();
                foreach (var animal in animalProvider.createdAnimals.Values)
                {
                    animals.Add(animal);
                }
        
                /*JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Converters = { new JsonAnimalConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(animals, options);
                File.WriteAllText(filePath, jsonString);*/
                
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Animals>), knownType);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    jsonFormatter.WriteObject(stream, animals);
                }
                MessageBox.Show("Объекты успешно сериализованы в файл JSON.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сериализации: {ex.Message}");
            }
        }

        private List<Animals> JDeserializeAnimals(string filePath)
        {
            try
            {
                /*byte[] jsonBytes = File.ReadAllBytes(filePath);
                string jsonString = Encoding.UTF8.GetString(jsonBytes);
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonAnimalConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };*/

                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Animals>), knownType);
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    return (List<Animals>)jsonFormatter.ReadObject(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при десериализации: {ex.Message}");
                return null;
            }
        }
    }
}