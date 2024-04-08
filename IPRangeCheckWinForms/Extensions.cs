using System.Text;

namespace IPRangeCheckWinForms
{
    internal static class Extensions
    {

        public static string GetDataGridViewArgimentCommandLine(this DataGridView dataGridView)
        {
            StringBuilder output = new StringBuilder();
            foreach (DataGridViewRow row in dataGridView.Rows)
                output.Append(row.Cells[0].Value?.ToString()).Append("=").Append(row.Cells[1].Value?.ToString()).Append(" ");
            return output.ToString();
        }

    }
}
