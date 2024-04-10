using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Text.Json;
using System.Runtime.Serialization;
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
        private Bitmap[] catImage;
        private Bitmap[] dogImage;
        private Bitmap[] parrotImage;
        private List<PictureBox> picturesBox= new List<PictureBox>();
        private Point startPosition;
        private Size startSize;
        private Rectangle selectionRectangle;
        private Random random;
        private Dictionary<string, Control[]> animalControls;
        private TextBox NameTextBox;
        private TextBox AgeTextBox;
        private TextBox catBreedTextBox;
        private TextBox catNumberOfLegsTextBox;
        
        private TextBox dogBreedTextBox;
        private TextBox dogNumberOfLegsTextBox;
        private TextBox dogCommandsTextBox;
        
        private TextBox parrotWingspanTextBox;
        private TextBox parrotFeathersTextBox;
        private TextBox parrotWordsTextBox;

        private TextBox additionalTextBox;

        private AnimalProvider animalProvider = new AnimalProvider();

        private List<Animals> animals = new List<Animals>();

        public Form1()
        {
            InitializeComponent();
            InitializeAnimalControls();
            
            random = new Random();
            catImage = new Bitmap[]
            {
                Properties.Resources.cat1,Properties.Resources.cat2,Properties.Resources.cat3,
                Properties.Resources.cat4,Properties.Resources.cat5
            };
            dogImage = new Bitmap[]
            {
                Properties.Resources.dog1,Properties.Resources.dog2,Properties.Resources.dog3,
                Properties.Resources.dog4,Properties.Resources.dog5
            };
            parrotImage = new Bitmap[]
            {
                Properties.Resources.parrot1, Properties.Resources.parrot2, Properties.Resources.parrot3,
                Properties.Resources.parrot4, Properties.Resources.parrot5
            };
            
            additionalActionsComboBox = new ComboBox();
            additionalActionsComboBox.Items.Add("Move");
            additionalActionsComboBox.Items.Add("Make sound");
            additionalActionsComboBox.Items.Add("Feed the cat");
            additionalActionsComboBox.Items.Add("Add command to the dog");
            additionalActionsComboBox.Items.Add("Display commands");
            additionalActionsComboBox.Items.Add("Teach a word to the parrot");
            additionalActionsComboBox.SelectedIndex = 0;
            additionalActionsComboBox.Location = new Point(110, 205);
            Controls.Add(additionalActionsComboBox);

            performActionButton = new Button();
            performActionButton.Text = "Perform";
            performActionButton.Location = new Point(130, 240);
            performActionButton.Click += PerformActionButton_Click;
            Controls.Add(performActionButton);
            
            serializeBButton = new Button();
            serializeBButton.Text = "Binary Serialize";
            serializeBButton.Location = new Point(130, 30);
            serializeBButton.Size = new Size(100, 25);
            serializeBButton.Click += buttonBSerialize_Click;
            Controls.Add(serializeBButton);
            
            deserializeBButton = new Button();
            deserializeBButton.Text = "Binary Deserialize";
            deserializeBButton.Location = new Point(130, 60);
            deserializeBButton.Size = new Size(100, 25);
            deserializeBButton.Click += buttonBDeserialize_Click;
            Controls.Add(deserializeBButton);
            
            serializeJButton = new Button();
            serializeJButton.Text = "JSON Serialize";
            serializeJButton.Location = new Point(130, 90);
            serializeJButton.Size = new Size(100, 25);
            serializeJButton.Click += buttonJSerialize_Click;
            Controls.Add(serializeJButton);
            
            deserializeJButton = new Button();
            deserializeJButton.Text = "JSON Deserialize";
            deserializeJButton.Location = new Point(130, 120);
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
            animalComboBox.Items.Add("Cat");
            animalComboBox.Items.Add("Dog");
            animalComboBox.Items.Add("Parrot");
            animalComboBox.SelectedIndex = 0;
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
        }
        
        private void InitializeAnimalControls()
        {
            animalControls = new Dictionary<string, Control[]>();

            Label NameLabel = new Label();
            NameLabel.Text = "Name:";
            NameTextBox = new TextBox();
            Label AgeLabel = new Label();
            AgeLabel.Text = "Age:";
            AgeTextBox = new TextBox();
            
            Label catBreedLabel = new Label();
            catBreedLabel.Text = "Breed:";
            catBreedTextBox = new TextBox();
            Label catNumberOfLegsLabel = new Label();
            catNumberOfLegsLabel.Text = "Number of legs:";
            catNumberOfLegsTextBox = new TextBox();
            
            Label dogBreedLabel = new Label();
            dogBreedLabel.Text = "Breed:";
            dogBreedTextBox = new TextBox();
            Label dogNumberOfLegsLabel = new Label();
            dogNumberOfLegsLabel.Text = "Number of legs:";
            dogNumberOfLegsTextBox = new TextBox();
            Label dogCommandsLabel = new Label();
            dogCommandsLabel.Text = "Commands:";
            dogCommandsTextBox = new TextBox();
            
            Label parrotWingspanLabel = new Label();
            parrotWingspanLabel.Text = "Wingspan:";
            parrotWingspanTextBox = new TextBox();
            Label parrotFeathersLabel = new Label();
            parrotFeathersLabel.Text = "Type of feathers:";
            parrotFeathersTextBox = new TextBox();
            Label parrotWordsLabel = new Label();
            parrotWordsLabel.Text = "Words:";
            parrotWordsTextBox = new TextBox();
            
            animalControls.Add("Cat", new Control[] { NameLabel, NameTextBox, AgeLabel, AgeTextBox, catBreedLabel, catBreedTextBox, catNumberOfLegsLabel, catNumberOfLegsTextBox });
            animalControls.Add("Dog", new Control[] { NameLabel, NameTextBox, AgeLabel, AgeTextBox, dogBreedLabel, dogBreedTextBox, dogNumberOfLegsLabel, dogNumberOfLegsTextBox, dogCommandsLabel, dogCommandsTextBox });
            animalControls.Add("Parrot", new Control[] { NameLabel, NameTextBox, AgeLabel, AgeTextBox, parrotWingspanLabel, parrotWingspanTextBox, parrotFeathersLabel, parrotFeathersTextBox, parrotWordsLabel, parrotWordsTextBox });
            
            foreach (var controls in animalControls.Values)
            {
                foreach (var control in controls)
                {
                    control.Visible = false;
                    Controls.Add(control);
                }
            }
            
            NameLabel.Location = new Point(5, 5);
            NameTextBox.Location = new Point(5, 25);
            AgeLabel.Location = new Point(5, 50);
            AgeTextBox.Location = new Point(5, 70);
            catBreedLabel.Location = new Point(5, 95);
            catBreedTextBox.Location = new Point(5, 115);
            catNumberOfLegsLabel.Location = new Point(5, 140);
            catNumberOfLegsTextBox.Location = new Point(5, 160);
            
            dogBreedLabel.Location = new Point(5, 95);
            dogBreedTextBox.Location = new Point(5, 115);
            dogNumberOfLegsLabel.Location = new Point(5, 140);
            dogNumberOfLegsTextBox.Location = new Point(5, 160);
            dogCommandsLabel.Location = new Point(5, 185);
            dogCommandsTextBox.Location = new Point(5, 205);
            
            parrotWingspanLabel.Location = new Point(5, 95);
            parrotWingspanTextBox.Location = new Point(5, 115);
            parrotFeathersLabel.Location = new Point(5, 140);
            parrotFeathersTextBox.Location = new Point(5, 160);
            parrotWordsLabel.Location = new Point(5, 185);
            parrotWordsTextBox.Location = new Point(5, 205);
            
            editButton = new Button();
            editButton.Text = "Edit";
            editButton.Location = new Point(10, 270);
            editButton.Click += EditButton_Click;
            Controls.Add(editButton);

            deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.Location = new Point(90, 270);
            deleteButton.Click += DeleteButton_Click;
            Controls.Add(deleteButton);
            
            ShowAnimalControls("Cat");
        }

        private void animalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAnimal = animalComboBox.SelectedItem.ToString();
            ShowAnimalControls(selectedAnimal);
        }

        private void ShowAnimalControls(string animalType)
        {
            foreach (var controls in animalControls.Values)
            {
                foreach (var control in controls)
                {
                    control.Visible = false;
                }
            }

            foreach (var control in animalControls[animalType])
            {
                control.Visible = true;
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string selectedAnimal = animalComboBox.SelectedItem.ToString();
            string name = NameTextBox.Text;
            int age = int.Parse(AgeTextBox.Text);
            Animals animal;
                Dictionary<string, string> additionalInfo = new Dictionary<string, string>();

                switch (selectedAnimal)
                {
                    case "Cat":
                        additionalInfo.Add("Breed", catBreedTextBox.Text);
                        additionalInfo.Add("NumberOfLegs", catNumberOfLegsTextBox.Text);
                        DisplayRandomImage(catImage);
                        break;
                    case "Dog":
                        additionalInfo.Add("Breed", dogBreedTextBox.Text);
                        additionalInfo.Add("NumberOfLegs", dogNumberOfLegsTextBox.Text);
                        additionalInfo.Add("Commands", dogCommandsTextBox.Text);
                        DisplayRandomImage(dogImage);
                        break;
                    case "Parrot":
                        additionalInfo.Add("Wingspan", parrotWingspanTextBox.Text);
                        additionalInfo.Add("FeathersType", parrotFeathersTextBox.Text);
                        additionalInfo.Add("Words", parrotWordsTextBox.Text);
                        DisplayRandomImage(parrotImage);
                        break;
                    default:
                        break;
                }

                IAnimalFactory animalFactory = GetAnimalFactory(selectedAnimal);
                animal = animalFactory.CreateAnimal(name, age, additionalInfo);
                
                animalProvider[name] = animal;
                
                if (animal != null)
                {
                    animalListBox.Items.Add(name);
                }
        }
        
        private IAnimalFactory GetAnimalFactory(string animalType)
        {
            switch (animalType)
            {
                case "Cat":
                    return new CatFactory();
                case "Dog":
                    return new DogFactory();
                case "Parrot":
                    return new ParrotFactory();
                default:
                    throw new NotSupportedException($"Animal type '{animalType}' is not supported.");
            }
        }

        private void DisplayRandomImage(Bitmap[] images)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(random.Next(300, 700), random.Next(0, 400));
            pictureBox.Size = new Size(100, 100);
            int index = random.Next(0, 5);
            pictureBox.Image = images[index];
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = Color.Transparent;
            picturesBox.Add(pictureBox);
            Controls.Add(pictureBox);
            pictureBox.BringToFront();
        }

        private void PerformActionButton_Click(object sender, EventArgs eventArgs)
        {
            string selectedAction = additionalActionsComboBox.SelectedItem.ToString();
            string additional = additionalTextBox.Text;
            Animals animal = animalProvider[animalListBox.SelectedItem.ToString()];
            switch (selectedAction)
            {
                case "Feed the cat":
                    if (animal is Cat cat)
                    {
                        cat.Feed();
                    }
                    break;
                case "Add command to the dog":
                    if (animal is Dog dog)
                    {
                        dog.AddCommand(additional);
                    }
                    break;
                case "Teach a word to the parrot":
                    if (animal is Parrot parrot)
                    {
                        parrot.AddWord(additional);
                    }
                    break;
                case "Display commands":
                    if (animal is Dog dog1)
                    {
                        Console.WriteLine(dog1.Commands);
                    }
                    break;
                case "Move":
                    animal.Move();
                    break;
                case "Make sound":
                    animal.MakeSound();
                    break;
                default:
                    break;
            }
        }
        private void buttonBSerialize_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Binary files (*.dat)|*.dat|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    BSerializeAnimals(saveFileDialog.FileName);
                }
            }
        }

        private void buttonBDeserialize_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Binary files (*.dat)|*.dat|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    animals = BDeserializeAnimals(openFileDialog.FileName);
                    if (animals != null)
                    {
                        animalListBox.Items.Clear();
                        foreach (var picture in picturesBox)
                        {
                            picture.Dispose();
                        }
                        foreach (Animals animal in animals)
                        {
                            animalListBox.Items.Add(animal.Name);
                            animalProvider[animal.Name] = animal;
                            if(animal is Cat) DisplayRandomImage(catImage);
                            else if (animal is Dog) DisplayRandomImage(dogImage);
                            else if (animal is Parrot) DisplayRandomImage(parrotImage);
                        }
                        MessageBox.Show("Объекты успешно загружены из файла.");
                    }
                }
            }
        }
        
        private void BSerializeAnimals(string filePath)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    animals.Clear();
                    foreach (var animal in animalProvider.createdAnimals.Values)
                    {
                        animals.Add(animal);
                    }
                    formatter.Serialize(fs, animals);
                    MessageBox.Show("Объекты успешно сохранены в файл.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сериализации: {ex.Message}");
            }
        }

        private List<Animals> BDeserializeAnimals(string filePath)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<Animals>)formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при десериализации: {ex.Message}");
                return null;
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
                                if(animal is Cat) DisplayRandomImage(catImage);
                                else if (animal is Dog) DisplayRandomImage(dogImage);
                                else if (animal is Parrot) DisplayRandomImage(parrotImage);
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
                
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Animals>));
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
                
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Animals>));
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    animals = (List<Animals>)jsonFormatter.ReadObject(stream);
                }

                return animals;
                //return JsonSerializer.Deserialize<List<Animals>>(jsonString, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при десериализации: {ex.Message}");
                return null;
            }
        }
        
        private void EditButton_Click(object sender, EventArgs e)
        {
            if (animalListBox.SelectedItem != null)
            {
                string selectedAnimalName = animalListBox.SelectedItem.ToString();
                Animals selectedAnimal = animalProvider[selectedAnimalName];

                ShowAnimalControls(selectedAnimal.GetType().Name);
                
                NameTextBox.Text = selectedAnimal.Name;
                AgeTextBox.Text = selectedAnimal.Age.ToString();
        
                if (selectedAnimal is Cat cat)
                {
                    catBreedTextBox.Text = cat.Breed;
                    catNumberOfLegsTextBox.Text = cat.NumberOfLegs.ToString();
                }
                else if (selectedAnimal is Dog dog)
                {
                    dogBreedTextBox.Text = dog.Breed;
                    dogNumberOfLegsTextBox.Text = dog.NumberOfLegs.ToString();
                    dogCommandsTextBox.Text = string.Join(",", dog.Commands);
                }
                else if (selectedAnimal is Parrot parrot)
                {
                    parrotWingspanTextBox.Text = parrot.Wingspan.ToString();
                    parrotFeathersTextBox.Text = parrot.TypeOfFeathers;
                    parrotWordsTextBox.Text = string.Join(",", parrot.Words);
                }
                
                animalProvider.createdAnimals.Remove(selectedAnimalName);
                animalListBox.Items.Remove(selectedAnimalName);
            }
        }
        
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (animalListBox.SelectedItem != null)
            {
                string selectedAnimalName = animalListBox.SelectedItem.ToString();
                animalProvider.createdAnimals.Remove(selectedAnimalName);
                animalListBox.Items.Remove(selectedAnimalName);
            }
        }
    }
}