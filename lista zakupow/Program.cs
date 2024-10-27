using System;
using System.Windows.Forms;

namespace ShoppingList
{
    public class ShoppingListForm : Form
    {
        private TextBox itemTextBox;
        private TextBox quantityTextBox;
        private ListBox shoppingListBox;

        public ShoppingListForm()
        {
            Text = "Lista zakupów";
            Width = 400;
            Height = 350;

            Label itemLabel = new Label { Text = "Przedmiot:", Top = 10, Left = 10, Width = 80 };
            Controls.Add(itemLabel);

            itemTextBox = new TextBox { Top = 10, Left = 100, Width = 200 };
            Controls.Add(itemTextBox);

            Label quantityLabel = new Label { Text = "Iloœæ:", Top = 40, Left = 10, Width = 80 };
            Controls.Add(quantityLabel);

            quantityTextBox = new TextBox { Top = 40, Left = 100, Width = 200 };
            Controls.Add(quantityTextBox);

            Button addButton = new Button { Text = "Dodaj", Top = 40, Left = 310 };
            addButton.Click += AddButton_Click;
            Controls.Add(addButton);

            shoppingListBox = new ListBox { Top = 70, Left = 10, Width = 360, Height = 170 };
            shoppingListBox.MouseDoubleClick += ShoppingListBox_MouseDoubleClick;
            Controls.Add(shoppingListBox);

            Button clearButton = new Button { Text = "Wyczyœæ", Top = 250, Left = 310 };
            clearButton.Click += ClearButton_Click;
            Controls.Add(clearButton);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemTextBox.Text))
                    throw new Exception("Pole przedmiotu nie mo¿e byæ puste.");

                if (string.IsNullOrWhiteSpace(quantityTextBox.Text))
                    throw new Exception("Pole iloœci nie mo¿e byæ puste.");

                if (!int.TryParse(quantityTextBox.Text, out int quantity) || quantity <= 0)
                    throw new Exception("Iloœæ musi byæ dodatni¹ liczb¹ ca³kowit¹.");

                shoppingListBox.Items.Add(new ShoppingItem(itemTextBox.Text, quantity));
                itemTextBox.Clear();
                quantityTextBox.Clear();
                itemTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno chcesz wyczyœciæ listê zakupów?", "Potwierdzenie", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                shoppingListBox.Items.Clear();
        }

        private void ShoppingListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = shoppingListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                shoppingListBox.Items.RemoveAt(index);
            }
        }
    }

    public class ShoppingItem
    {
        public string Item { get; }
        public int Quantity { get; }

        public ShoppingItem(string item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public override string ToString() => $"{Item} ({Quantity})";
    }

    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ShoppingListForm());
        }
    }
}
