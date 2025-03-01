using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Demo_Forms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                INSERT INTO form (id, name, created, modified) VALUES ('F30D18BE-1729-4217-8F27-D2D1AEF6F72D', 'Register form', '2025-03-01 17:41:04.375', '2025-03-01 17:56:00.2378255');
                INSERT INTO form (id, name, created, modified) VALUES ('69CE10C6-6C1E-46F7-8955-7AAA584D6144', 'Login form', '2025-03-01 17:49:31.124', '2025-03-01 17:52:26.6858637');
                INSERT INTO form (id, name, created, modified) VALUES ('BA41A6A6-2E91-4A20-A5B1-7B2BA615091C', 'Newsletter', '2025-03-01 17:52:46.392', '2025-03-01 17:54:28.057116');

                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('94787C70-D201-4597-B0DA-25C27F845D41', 'Email', null, 1, 'F30D18BE-1729-4217-8F27-D2D1AEF6F72D', 'text', null, null, null, 'email', null, null, null, null, null, null, 'g@nachkebia.dev', 'name@example.com', 1, null, '', null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('D037E116-AC10-4FCF-96F9-9BDD56D0A14A', 'Full name', '', 1, 'F30D18BE-1729-4217-8F27-D2D1AEF6F72D', 'text', null, null, null, 'text', null, null, null, null, null, null, 'Giorgi Torvalds', null, 0, null, null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('3BBDF61D-14A8-439C-A39F-892331156C01', 'Desired role', null, 1, 'F30D18BE-1729-4217-8F27-D2D1AEF6F72D', 'select', null, null, null, null, null, null, null, null, null, '["User","Staff","Accountant","Admin"]', null, null, 2, null, null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('3A2098C4-71BD-4641-B1F0-9921FEBAC7B1', 'Date of birth', 'You must be at least 18 years old to use our services', 1, 'F30D18BE-1729-4217-8F27-D2D1AEF6F72D', 'date', null, null, '2025-03-01 17:44:12.407', 'date', null, null, null, null, null, null, null, null, 3, null, null, '2004-12-31 20:00:00', '1969-12-31 20:00:00');
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('0473EEAE-673F-49F8-AE0F-A839D797C3BB', '', null, 0, 'F30D18BE-1729-4217-8F27-D2D1AEF6F72D', 'button', 'Register', null, null, null, null, null, null, null, null, null, null, null, 5, 'submit', null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('9330C463-21AB-4713-B123-D7A4D1D02864', 'Agree to our terms of services?', null, 1, 'F30D18BE-1729-4217-8F27-D2D1AEF6F72D', 'switch', null, 0, null, null, null, null, null, null, null, null, null, null, 4, null, null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('AD2D8D65-7BA0-483F-A6E1-F8141A01218E', 'Username', 'Only letters, periods and underscores allowed', 1, '69CE10C6-6C1E-46F7-8955-7AAA584D6144', 'text', null, null, null, 'text', null, null, null, null, null, null, null, 'Enter username...', 0, null, '/[a-z\.\_]/', null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('BF4B0EA1-F46D-4C8F-8CA6-F7817C733791', 'Password', null, 1, '69CE10C6-6C1E-46F7-8955-7AAA584D6144', 'text', null, null, null, 'text', null, null, null, null, null, null, null, '***', 1, null, null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('2DDD4847-B478-4E87-B59A-32218DAB1BA8', 'Remember me?', null, 0, '69CE10C6-6C1E-46F7-8955-7AAA584D6144', 'switch', null, 0, null, null, null, null, null, null, null, null, null, null, 2, null, null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('229E37CF-7172-4DF2-8F0C-1D2D0B24054B', '', null, 0, '69CE10C6-6C1E-46F7-8955-7AAA584D6144', 'button', 'Login', null, null, null, null, null, null, null, null, null, null, null, 3, 'submit', null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('4147BADB-1A2A-4312-8996-5D5999D0B291', '', null, 0, 'BA41A6A6-2E91-4A20-A5B1-7B2BA615091C', 'button', 'Sign up to our newsletter!', null, null, null, null, null, null, null, null, null, null, null, 1, 'submit', null, null, null);
                INSERT INTO base_component_choice (id, label, description, required, form_id, type, button_text, checked, selected_date, input_type, selected_numeric_value, min, max, step, selected_option, choices, text_value, placeholder, "order", button_type, pattern, date_max, date_min) VALUES ('F53F6135-7739-4702-A463-4C6BD84793D4', 'Email', null, 1, 'BA41A6A6-2E91-4A20-A5B1-7B2BA615091C', 'text', null, null, null, 'email', null, null, null, null, null, null, null, 'name@example.com', 0, null, null, null, null);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM form WHERE id = 'F30D18BE-1729-4217-8F27-D2D1AEF6F72D';
                DELETE FROM form WHERE id = '69CE10C6-6C1E-46F7-8955-7AAA584D6144';
                DELETE FROM form WHERE id = 'BA41A6A6-2E91-4A20-A5B1-7B2BA615091C';

                DELETE FROM base_component_choice WHERE id = '94787C70-D201-4597-B0DA-25C27F845D41';
                DELETE FROM base_component_choice WHERE id = 'D037E116-AC10-4FCF-96F9-9BDD56D0A14A';
                DELETE FROM base_component_choice WHERE id = '3BBDF61D-14A8-439C-A39F-892331156C01';
                DELETE FROM base_component_choice WHERE id = '3A2098C4-71BD-4641-B1F0-9921FEBAC7B1';
                DELETE FROM base_component_choice WHERE id = '0473EEAE-673F-49F8-AE0F-A839D797C3BB';
                DELETE FROM base_component_choice WHERE id = '9330C463-21AB-4713-B123-D7A4D1D02864';
                DELETE FROM base_component_choice WHERE id = 'AD2D8D65-7BA0-483F-A6E1-F8141A01218E';
                DELETE FROM base_component_choice WHERE id = 'BF4B0EA1-F46D-4C8F-8CA6-F7817C733791';
                DELETE FROM base_component_choice WHERE id = '2DDD4847-B478-4E87-B59A-32218DAB1BA8';
                DELETE FROM base_component_choice WHERE id = '229E37CF-7172-4DF2-8F0C-1D2D0B24054B';
                DELETE FROM base_component_choice WHERE id = '4147BADB-1A2A-4312-8996-5D5999D0B291';
                DELETE FROM base_component_choice WHERE id = 'F53F6135-7739-4702-A463-4C6BD84793D4';
                """);
        }
    }
}
