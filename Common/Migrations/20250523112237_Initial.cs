using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Common.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "client_template");

            migrationBuilder.CreateTable(
                name: "chat_escalation_status",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_escalation_status_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_report_format",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_format_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_report_template_column_type",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_template_column_type_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "document",
                schema: "client_template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Embeddings = table.Column<float[]>(type: "real[]", nullable: false),
                    Xmin = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("documents_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "google_credential",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_account = table.Column<string>(type: "text", nullable: false),
                    private_key = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    n8n_credential_id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("google_credential_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "human_agent",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("human_agent_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "industry",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("industry_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "instructions",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("instructions_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "openai_credential",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    model_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    n8n_credential_id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("openai_credential_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rule",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rule_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rule_set",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rule_set_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subscription_type",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("subscription_type_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "twilio_credential",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    n8n_credential_id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    sid = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("twilio_credential_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workflow_template",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    json = table.Column<string>(type: "json", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workflow_template_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_report_template",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    format_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_template_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_report_template_format_fk",
                        column: x => x.format_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report_format",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_report_instance_google_sheets",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    credential_id = table.Column<int>(type: "integer", nullable: false),
                    SheetName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_instance_google_sheets_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_report_format_google_sheets_credential_fk",
                        column: x => x.credential_id,
                        principalSchema: "client_template",
                        principalTable: "google_credential",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    services = table.Column<string>(type: "text", nullable: true),
                    website = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    company_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    industry_id = table.Column<int>(type: "integer", nullable: false),
                    facebook_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    tiktok_url = table.Column<string>(type: "text", maxLength: 0, nullable: true),
                    x_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    linkedin_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    instagram_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ideal_customer = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_demo = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("project_pk", x => x.id);
                    table.ForeignKey(
                        name: "project_industry_fk",
                        column: x => x.industry_id,
                        principalSchema: "client_template",
                        principalTable: "industry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instructions_section",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iid = table.Column<int>(type: "integer", nullable: false),
                    priority = table.Column<short>(type: "smallint", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    text = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("instructions_section_pk", x => x.id);
                    table.ForeignKey(
                        name: "instructions_section_instruction_fk",
                        column: x => x.iid,
                        principalSchema: "client_template",
                        principalTable: "instructions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instructions_rule",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iid = table.Column<int>(type: "integer", nullable: false),
                    rid = table.Column<int>(type: "integer", nullable: false),
                    priority = table.Column<short>(type: "smallint", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("instructions_rule_pk", x => x.id);
                    table.ForeignKey(
                        name: "instructions_rule_instruction_fk",
                        column: x => x.iid,
                        principalSchema: "client_template",
                        principalTable: "instructions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "instructions_rule_rule_fk",
                        column: x => x.rid,
                        principalSchema: "client_template",
                        principalTable: "rule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rule_set_rules",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rid = table.Column<int>(type: "integer", nullable: false),
                    rsid = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rule_set_rules_pk", x => x.id);
                    table.ForeignKey(
                        name: "rule_set_rules_rule_fk",
                        column: x => x.rid,
                        principalSchema: "client_template",
                        principalTable: "rule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "rule_set_rules_rule_set_fk",
                        column: x => x.rsid,
                        principalSchema: "client_template",
                        principalTable: "rule_set",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("subscription_pk", x => x.id);
                    table.ForeignKey(
                        name: "subscription_subscription_type_fk",
                        column: x => x.type_id,
                        principalSchema: "client_template",
                        principalTable: "subscription_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_report_template_column",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_template_column_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_report_template_column_template_fk",
                        column: x => x.template_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chat_report_template_column_type_fk",
                        column: x => x.type_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report_template_column_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_report_instance",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    google_sheets_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_instance_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_report_instance_google_sheets_fk",
                        column: x => x.google_sheets_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report_instance_google_sheets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chat_report_instance_template_fk",
                        column: x => x.template_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    subscription_id = table.Column<int>(type: "integer", nullable: false),
                    json = table.Column<string>(type: "json", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workflow_pk", x => x.id);
                    table.ForeignKey(
                        name: "workflow_project_fk",
                        column: x => x.project_id,
                        principalSchema: "client_template",
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "workflow_subscription_fk",
                        column: x => x.subscription_id,
                        principalSchema: "client_template",
                        principalTable: "subscription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "workflow_template_fk",
                        column: x => x.template_id,
                        principalSchema: "client_template",
                        principalTable: "workflow_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ai_agent",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    wid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    instructions_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ai_agent_pk", x => x.id);
                    table.ForeignKey(
                        name: "ai_agent_instructions_id_fk",
                        column: x => x.instructions_id,
                        principalSchema: "client_template",
                        principalTable: "instructions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ai_agent_workflow__fk",
                        column: x => x.wid,
                        principalSchema: "client_template",
                        principalTable: "workflow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    conversation_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    escalation_id = table.Column<int>(type: "integer", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chat_escalation",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_chat_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_escalation_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_chat_escalation_fk",
                        column: x => x.source_chat_id,
                        principalSchema: "client_template",
                        principalTable: "chat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chat_escalation_status_fk",
                        column: x => x.status_id,
                        principalSchema: "client_template",
                        principalTable: "chat_escalation_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_message",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cid = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_user = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_message_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_message_chat_fk",
                        column: x => x.cid,
                        principalSchema: "client_template",
                        principalTable: "chat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_report",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    instance_id = table.Column<int>(type: "integer", nullable: false),
                    cid = table.Column<int>(type: "integer", nullable: false),
                    data = table.Column<byte[]>(type: "bytea", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_report_chat_fk",
                        column: x => x.cid,
                        principalSchema: "client_template",
                        principalTable: "chat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chat_report_instance_fk",
                        column: x => x.instance_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report_instance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conversation",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    ai_agent_id = table.Column<int>(type: "integer", nullable: true),
                    human_agent_id = table.Column<int>(type: "integer", nullable: true),
                    last_chat_message_id = table.Column<int>(type: "integer", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("conversation_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_conversation_last_chat_message_fk",
                        column: x => x.last_chat_message_id,
                        principalSchema: "client_template",
                        principalTable: "chat_message",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "conversation_ai_agent_fk",
                        column: x => x.ai_agent_id,
                        principalSchema: "client_template",
                        principalTable: "ai_agent",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "conversation_human_agent_fk",
                        column: x => x.human_agent_id,
                        principalSchema: "client_template",
                        principalTable: "human_agent",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "conversation_user_fk",
                        column: x => x.user_id,
                        principalSchema: "client_template",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_report_column_value",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    column_id = table.Column<int>(type: "integer", nullable: false),
                    report_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<byte[]>(type: "bytea", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("chat_report_column_value_pk", x => x.id);
                    table.ForeignKey(
                        name: "chat_report_column_value_column_fk",
                        column: x => x.column_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report_template_column",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chat_report_column_value_report_fk",
                        column: x => x.report_id,
                        principalSchema: "client_template",
                        principalTable: "chat_report",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session",
                schema: "client_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    conversation_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("session_pk", x => x.id);
                    table.ForeignKey(
                        name: "session_conversation__fk",
                        column: x => x.conversation_id,
                        principalSchema: "client_template",
                        principalTable: "conversation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ai_agent_unique_key",
                schema: "client_template",
                table: "ai_agent",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ai_agent_instructions_id",
                schema: "client_template",
                table: "ai_agent",
                column: "instructions_id");

            migrationBuilder.CreateIndex(
                name: "IX_ai_agent_wid",
                schema: "client_template",
                table: "ai_agent",
                column: "wid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_conversation_id",
                schema: "client_template",
                table: "chat",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_escalation_source_chat_id",
                schema: "client_template",
                table: "chat_escalation",
                column: "source_chat_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chat_escalation_status_id",
                schema: "client_template",
                table: "chat_escalation",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_message_cid",
                schema: "client_template",
                table: "chat_message",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_cid",
                schema: "client_template",
                table: "chat_report",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_instance_id",
                schema: "client_template",
                table: "chat_report",
                column: "instance_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_column_value_column_id",
                schema: "client_template",
                table: "chat_report_column_value",
                column: "column_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_column_value_report_id",
                schema: "client_template",
                table: "chat_report_column_value",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "chat_report_format_unique_key",
                schema: "client_template",
                table: "chat_report_format",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_instance_google_sheets_id",
                schema: "client_template",
                table: "chat_report_instance",
                column: "google_sheets_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_instance_template_id",
                schema: "client_template",
                table: "chat_report_instance",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_instance_google_sheets_credential_id",
                schema: "client_template",
                table: "chat_report_instance_google_sheets",
                column: "credential_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_template_format_id",
                schema: "client_template",
                table: "chat_report_template",
                column: "format_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_template_column_template_id",
                schema: "client_template",
                table: "chat_report_template_column",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_report_template_column_type_id",
                schema: "client_template",
                table: "chat_report_template_column",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_conversation_ai_agent_id",
                schema: "client_template",
                table: "conversation",
                column: "ai_agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_conversation_human_agent_id",
                schema: "client_template",
                table: "conversation",
                column: "human_agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_conversation_last_chat_message_id",
                schema: "client_template",
                table: "conversation",
                column: "last_chat_message_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_conversation_user_id",
                schema: "client_template",
                table: "conversation",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "google_credential_unique_key",
                schema: "client_template",
                table: "google_credential",
                column: "n8n_credential_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "human_agent_unique_key",
                schema: "client_template",
                table: "human_agent",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "instructions_rule_unique_key",
                schema: "client_template",
                table: "instructions_rule",
                columns: new[] { "rid", "iid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_instructions_rule_iid",
                schema: "client_template",
                table: "instructions_rule",
                column: "iid");

            migrationBuilder.CreateIndex(
                name: "IX_instructions_section_iid",
                schema: "client_template",
                table: "instructions_section",
                column: "iid");

            migrationBuilder.CreateIndex(
                name: "openai_credential_unique_key",
                schema: "client_template",
                table: "openai_credential",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "openai_credential_unique_key2",
                schema: "client_template",
                table: "openai_credential",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "openai_credential_unique_key3",
                schema: "client_template",
                table: "openai_credential",
                column: "n8n_credential_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_industry_id",
                schema: "client_template",
                table: "project",
                column: "industry_id");

            migrationBuilder.CreateIndex(
                name: "project_unique_key",
                schema: "client_template",
                table: "project",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rule_set_rules_rsid",
                schema: "client_template",
                table: "rule_set_rules",
                column: "rsid");

            migrationBuilder.CreateIndex(
                name: "rule_set_rules_unique_key",
                schema: "client_template",
                table: "rule_set_rules",
                columns: new[] { "rid", "rsid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_session_conversation_id",
                schema: "client_template",
                table: "session",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscription_type_id",
                schema: "client_template",
                table: "subscription",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "subscription_unique_key",
                schema: "client_template",
                table: "subscription",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "subscription_type_unique_key",
                schema: "client_template",
                table: "subscription_type",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "twilio_credential_unique_key",
                schema: "client_template",
                table: "twilio_credential",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "twilio_credential_unique_key2",
                schema: "client_template",
                table: "twilio_credential",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "twilio_credential_unique_key3",
                schema: "client_template",
                table: "twilio_credential",
                column: "n8n_credential_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_unique_key",
                schema: "client_template",
                table: "user",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workflow_project_id",
                schema: "client_template",
                table: "workflow",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_subscription_id",
                schema: "client_template",
                table: "workflow",
                column: "subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_template_id",
                schema: "client_template",
                table: "workflow",
                column: "template_id");

            migrationBuilder.AddForeignKey(
                name: "chat_conversation_fk",
                schema: "client_template",
                table: "chat",
                column: "conversation_id",
                principalSchema: "client_template",
                principalTable: "conversation",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ai_agent_instructions_id_fk",
                schema: "client_template",
                table: "ai_agent");

            migrationBuilder.DropForeignKey(
                name: "ai_agent_workflow__fk",
                schema: "client_template",
                table: "ai_agent");

            migrationBuilder.DropForeignKey(
                name: "chat_conversation_fk",
                schema: "client_template",
                table: "chat");

            migrationBuilder.DropTable(
                name: "chat_escalation",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report_column_value",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "document",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "instructions_rule",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "instructions_section",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "openai_credential",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "rule_set_rules",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "session",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "twilio_credential",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_escalation_status",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report_template_column",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "rule",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "rule_set",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report_template_column_type",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report_instance",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report_instance_google_sheets",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report_template",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "google_credential",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_report_format",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "instructions",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "workflow",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "project",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "subscription",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "workflow_template",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "industry",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "subscription_type",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "conversation",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat_message",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "ai_agent",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "human_agent",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "user",
                schema: "client_template");

            migrationBuilder.DropTable(
                name: "chat",
                schema: "client_template");
        }
    }
}
