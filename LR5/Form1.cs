using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LR5
{
    // Клас будівлі
    public class Building : ICloneable
    {
        public List<string> Parts { get; private set; } = new List<string>();

        public void AddPart(string part)
        {
            Parts.Add(part);
        }

        public string Show()
        {
            return string.Join("\n", Parts);
        }

        // Реалізація патерну "Прототип"
        public object Clone()
        {
            var clone = new Building();
            foreach (var part in Parts)
            {
                clone.AddPart(part);
            }
            return clone;
        }
    }

    // Інтерфейс будівельника
    public interface IBuilder
    {
        void BuildFoundation();
        void BuildWalls();
        void BuildRoof();
        Building GetResult();
    }

    // Конкретний будівельник
    public class ConcreteBuilder : IBuilder
    {
        private Building _building = new Building();

        public void BuildFoundation()
        {
            _building.AddPart("Foundation built");
        }

        public void BuildWalls()
        {
            _building.AddPart("Walls constructed");
        }

        public void BuildRoof()
        {
            _building.AddPart("Roof added");
        }

        public Building GetResult()
        {
            return _building;
        }
    }

    // Директор
    public class Director
    {
        private IBuilder _builder;

        public Director(IBuilder builder)
        {
            _builder = builder;
        }

        public void Construct()
        {
            _builder.BuildFoundation();
            _builder.BuildWalls();
            _builder.BuildRoof();
        }
    }

    // Візуальний інтерфейс
    public partial class Form1 : Form
    {
        private IBuilder builder = new ConcreteBuilder();
        private Director director;
        private Building lastBuilding;

        public Form1()
        {
            InitializeComponent();
            director = new Director(builder);
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            builder = new ConcreteBuilder();
            director = new Director(builder);

            lstSteps.Items.Clear();
            lstSteps.Items.Add("Step 1: Building foundation...");
            builder.BuildFoundation();

            lstSteps.Items.Add("Step 2: Constructing walls...");
            builder.BuildWalls();

            lstSteps.Items.Add("Step 3: Adding roof...");
            builder.BuildRoof();

            lstSteps.Items.Add("Construction complete!");
            lastBuilding = builder.GetResult(); // Зберігаємо створену будівлю
            MessageBox.Show(lastBuilding.Show(), "Building Details");
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            if (lastBuilding != null)
            {
                var clonedBuilding = (Building)lastBuilding.Clone();
                MessageBox.Show(clonedBuilding.Show(), "Cloned Building Details");
            }
            else
            {
                MessageBox.Show("No building to clone!", "Error");
            }
        }
    }
}
