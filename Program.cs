using SistemaDeAgendamentoMedico2.Models;
using SistemaDeAgendamentoMedico2.Models.SistemaAgendamentoMedico.Models;
using System;
using System.Collections.Generic;

namespace SistemaDeAgendamentoMedico2
{

class Program
    {
        static void Main(string[] args)
        {
            GestorClinica gestor = new GestorClinica();
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\nSistema de Agendamento Médico ");
                Console.WriteLine("1 - Adicionar Médico");
                Console.WriteLine("2 - Adicionar Paciente");
                Console.WriteLine("3 - Marcar Consulta");
                Console.WriteLine("4 - Cancelar Consulta");
                Console.WriteLine("5 - Listar Consultas do Dia");
                Console.WriteLine("6 - Histórico de Paciente");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            Console.Write("Nome do médico: ");
                            string nomeMedico = Console.ReadLine();
                            Console.Write("Especialidade: ");
                            string especialidade = Console.ReadLine();

                            Medico medico = new Medico { Nome = nomeMedico, Especialidade = especialidade };
                            medico.HorariosDisponiveis.Add(DateTime.Now.AddHours(1)); // exemplo de horário
                            gestor.AdicionarMedico(medico);

                            Console.WriteLine("Médico adicionado com sucesso!");
                            break;

                        case "2":
                            Console.Write("Nome do paciente: ");
                            string nomePaciente = Console.ReadLine();
                            Console.Write("Contacto: ");
                            string contacto = Console.ReadLine();

                            Paciente paciente = new Paciente { Nome = nomePaciente, Contacto = contacto };
                            gestor.AdicionarPaciente(paciente);

                            Console.WriteLine("Paciente adicionado com sucesso!");
                            break;

                        case "3":
                            Console.WriteLine("Escolha um médico:");
                            List<Medico> medicos = gestor.ObterMedicos();
                            for (int i = 0; i < medicos.Count; i++)
                                Console.WriteLine($"{i} - {medicos[i].Nome} ({medicos[i].Especialidade})");

                            int indiceMedico = int.Parse(Console.ReadLine());

                            Console.WriteLine("Escolha um paciente:");
                            List<Paciente> pacientes = gestor.ObterPacientes();
                            for (int i = 0; i < pacientes.Count; i++)
                                Console.WriteLine($"{i} - {pacientes[i].Nome}");

                            int indicePaciente = int.Parse(Console.ReadLine());

                            Console.Write("Data e hora da consulta (ex: 2026-05-24 14:00): ");
                            DateTime dataHora = DateTime.Parse(Console.ReadLine());

                            gestor.MarcarConsulta(medicos[indiceMedico], pacientes[indicePaciente], dataHora);
                            Console.WriteLine("Consulta marcada com sucesso!");
                            break;

                        case "4":
                            Console.WriteLine("Cancelar consulta ainda precisa de lógica para escolher qual cancelar.");
                            break;

                        case "5":
                            Console.Write("Digite a data (ex: 2026-05-24): ");
                            DateTime dia = DateTime.Parse(Console.ReadLine());
                            var consultasDia = gestor.ListarConsultasDoDia(dia);

                            foreach (var c in consultasDia)
                                Console.WriteLine($"{c.DataHora} - {c.Medico.Nome} com {c.Paciente.Nome} ({c.Estado})");
                            break;

                        case "6":
                            Console.WriteLine("Escolha um paciente:");
                            List<Paciente> listaPacientes = gestor.ObterPacientes();
                            for (int i = 0; i < listaPacientes.Count; i++)
                                Console.WriteLine($"{i} - {listaPacientes[i].Nome}");

                            int indiceHist = int.Parse(Console.ReadLine());
                            var historico = gestor.HistoricoPaciente(listaPacientes[indiceHist]);

                            foreach (var c in historico)
                                Console.WriteLine($"{c.DataHora} - {c.Medico.Nome} ({c.Estado})");
                            break;

                        case "0":
                            continuar = false;
                            break;

                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }
    }
}