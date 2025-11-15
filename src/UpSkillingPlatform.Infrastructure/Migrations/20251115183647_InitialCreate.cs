using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UpSkillingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "competencias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categoria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_competencias", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trilhas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nivel = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    carga_horaria = table.Column<int>(type: "int", nullable: false),
                    foco_principal = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trilhas", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    area_atuacao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nivel_carreira = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trilha_competencia",
                columns: table => new
                {
                    trilha_id = table.Column<long>(type: "bigint", nullable: false),
                    competencia_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trilha_competencia", x => new { x.trilha_id, x.competencia_id });
                    table.ForeignKey(
                        name: "fk_trilha_competencia_competencia",
                        column: x => x.competencia_id,
                        principalTable: "competencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_trilha_competencia_trilha",
                        column: x => x.trilha_id,
                        principalTable: "trilhas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "matriculas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    trilha_id = table.Column<long>(type: "bigint", nullable: false),
                    data_inscricao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matriculas", x => x.id);
                    table.ForeignKey(
                        name: "fk_matricula_trilha",
                        column: x => x.trilha_id,
                        principalTable: "trilhas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_matricula_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "competencias",
                columns: new[] { "id", "categoria", "descricao", "nome" },
                values: new object[,]
                {
                    { 1L, "Tecnologia", "Machine Learning, Deep Learning, NLP", "Inteligência Artificial" },
                    { 2L, "Tecnologia", "Data Science, Business Intelligence, Big Data", "Análise de Dados" },
                    { 3L, "Tecnologia", "AWS, Azure, Google Cloud", "Cloud Computing" },
                    { 4L, "Tecnologia", "Frontend e Backend moderno", "Desenvolvimento Web" },
                    { 5L, "Humana", "Comunicação clara e assertiva", "Comunicação Efetiva" },
                    { 6L, "Humana", "Colaboração e cooperação", "Trabalho em Equipe" },
                    { 7L, "Humana", "Análise e resolução de problemas", "Pensamento Crítico" },
                    { 8L, "Gestão", "Metodologias ágeis e tradicionais", "Gestão de Projetos" }
                });

            migrationBuilder.InsertData(
                table: "trilhas",
                columns: new[] { "id", "carga_horaria", "descricao", "foco_principal", "nivel", "nome" },
                values: new object[,]
                {
                    { 1L, 40, "Aprenda os conceitos fundamentais de IA e como aplicar algoritmos de ML", "IA", "INICIANTE", "Fundamentos de IA e Machine Learning" },
                    { 2L, 120, "Torne-se um cientista de dados completo com Python e ferramentas modernas", "Dados", "INTERMEDIARIO", "Cientista de Dados Profissional" },
                    { 3L, 80, "Domine arquiteturas cloud escaláveis e resilientes", "Cloud", "AVANCADO", "Arquitetura Cloud Avançada" },
                    { 4L, 30, "Desenvolva habilidades humanas essenciais para liderança e colaboração", "Soft Skills", "INICIANTE", "Soft Skills para o Futuro" }
                });

            migrationBuilder.InsertData(
                table: "trilha_competencia",
                columns: new[] { "competencia_id", "trilha_id" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 7L, 1L },
                    { 1L, 2L },
                    { 2L, 2L },
                    { 3L, 3L },
                    { 8L, 3L },
                    { 5L, 4L },
                    { 6L, 4L },
                    { 7L, 4L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_matriculas_trilha_id",
                table: "matriculas",
                column: "trilha_id");

            migrationBuilder.CreateIndex(
                name: "IX_matriculas_usuario_id",
                table: "matriculas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_trilha_competencia_competencia_id",
                table: "trilha_competencia",
                column: "competencia_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matriculas");

            migrationBuilder.DropTable(
                name: "trilha_competencia");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "competencias");

            migrationBuilder.DropTable(
                name: "trilhas");
        }
    }
}
