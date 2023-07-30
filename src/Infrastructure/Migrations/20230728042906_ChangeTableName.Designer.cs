﻿// <auto-generated />
using System;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230728042906_ChangeTableName")]
    partial class ChangeTableName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Assessments.Entities.SummativeAssessmentResult", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("SummativeAssessmentResultId");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateMarked")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("SummativeAssessmentResult");
                });

            modelBuilder.Entity("Domain.Assessments.FormativeAssessment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ConductedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("YearLevel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("formative_assessment", (string)null);
                });

            modelBuilder.Entity("Domain.Assessments.SummativeAssessment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ConductedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateScheduled")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PlanningNotes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ResultId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("YearLevel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.ToTable("SummativeAssessments");
                });

            modelBuilder.Entity("Domain.LessonPlanAggregate.LessonPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PlanningNotes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("LessonPlans");
                });

            modelBuilder.Entity("Domain.ReportAggregate.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("YearLevel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Domain.ResourceAggregate.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAssessment")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("StrandId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Domain.StudentAggregate.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.ContentDescriptor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("ContentDescriptorId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Strand")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Substrand")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Strand");

                    b.HasIndex("Substrand");

                    b.ToTable("ContentDescriptor");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Elaboration", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("Elaboration");

                    b.Property<Guid>("ContentDescriptor")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ContentDescriptor");

                    b.ToTable("Elaboration");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Strand", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("StrandId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("YearLevel")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("YearLevel");

                    b.ToTable("Strand");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Substrand", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("SubstrandId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Strand")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Strand");

                    b.ToTable("Substrand");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.YearLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("YearLevelId");

                    b.Property<string>("AchievementStandard")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Subject")
                        .HasColumnType("char(36)");

                    b.Property<string>("YearLevelDescription")
                        .HasColumnType("longtext");

                    b.Property<int>("YearLevelValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Subject");

                    b.ToTable("YearLevel");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("SubjectId");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Domain.TeacherAggregate.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Domain.TermPlannerAggregate.TermPlanner", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("TermEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TermNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("TermStart")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("TermPlanners");
                });

            modelBuilder.Entity("Domain.TimeTableAggregate.WeekPlanner", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("WeekStart")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("WeekPlanners");
                });

            modelBuilder.Entity("Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.YearPlannerAggregate.YearPlanner", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("YearLevel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("YearPlanners");
                });

            modelBuilder.Entity("Domain.Assessments.Entities.SummativeAssessmentResult", b =>
                {
                    b.OwnsOne("Domain.Assessments.Entities.AssessmentGrade", "Grade", b1 =>
                        {
                            b1.Property<Guid>("SummativeAssessmentResultId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Grade")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<Guid>("Id")
                                .HasColumnType("char(36)");

                            b1.Property<double?>("Percentage")
                                .HasColumnType("double");

                            b1.HasKey("SummativeAssessmentResultId");

                            b1.ToTable("SummativeAssessmentResult");

                            b1.WithOwner()
                                .HasForeignKey("SummativeAssessmentResultId");
                        });

                    b.Navigation("Grade")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Assessments.SummativeAssessment", b =>
                {
                    b.HasOne("Domain.Assessments.Entities.SummativeAssessmentResult", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Result");
                });

            modelBuilder.Entity("Domain.LessonPlanAggregate.LessonPlan", b =>
                {
                    b.OwnsMany("Domain.LessonPlanAggregate.Entities.LessonComment", "Comments", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("char(36)")
                                .HasColumnName("LessonCommentId");

                            b1.Property<bool>("Completed")
                                .HasColumnType("tinyint(1)");

                            b1.Property<DateTime?>("CompletedDateTime")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Content")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<DateTime>("CreatedDateTime")
                                .HasColumnType("datetime(6)");

                            b1.Property<Guid>("LessonId")
                                .HasColumnType("char(36)");

                            b1.Property<bool>("StruckThrough")
                                .HasColumnType("tinyint(1)");

                            b1.Property<DateTime>("UpdatedDateTime")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("Id");

                            b1.HasIndex("LessonId");

                            b1.ToTable("LessonComment");

                            b1.WithOwner()
                                .HasForeignKey("LessonId");
                        });

                    b.OwnsMany("Domain.Assessments.ValueObjects.AssessmentId", "AssessmentIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("LessonId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("AssessmentId");

                            b1.HasKey("Id");

                            b1.HasIndex("LessonId");

                            b1.ToTable("LessonAssessmentId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("LessonId");
                        });

                    b.OwnsMany("Domain.ResourceAggregate.ValueObjects.ResourceId", "ResourceIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("LessonId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("ResourceId");

                            b1.HasKey("Id");

                            b1.HasIndex("LessonId");

                            b1.ToTable("LessonResourceId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("LessonId");
                        });

                    b.Navigation("AssessmentIds");

                    b.Navigation("Comments");

                    b.Navigation("ResourceIds");
                });

            modelBuilder.Entity("Domain.ReportAggregate.Report", b =>
                {
                    b.OwnsMany("Domain.ReportAggregate.Entities.ReportComment", "ReportComments", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("char(36)");

                            b1.Property<int>("CharacterLimit")
                                .HasColumnType("int");

                            b1.Property<string>("Comments")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Grade")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<Guid>("ReportId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("SubjectId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("ReportId");

                            b1.ToTable("ReportComment");

                            b1.WithOwner()
                                .HasForeignKey("ReportId");
                        });

                    b.Navigation("ReportComments");
                });

            modelBuilder.Entity("Domain.StudentAggregate.Student", b =>
                {
                    b.OwnsMany("Domain.Assessments.ValueObjects.AssessmentId", "AssessmentIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("StudentId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("AssessmentId");

                            b1.HasKey("Id");

                            b1.HasIndex("StudentId");

                            b1.ToTable("StudentAssessmentId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsMany("Domain.ReportAggregate.ValueObjects.ReportCommentId", "ReportIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("StudentId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("ReportId");

                            b1.HasKey("Id");

                            b1.HasIndex("StudentId");

                            b1.ToTable("StudentReportId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsMany("Domain.SubjectAggregates.ValueObjects.SubjectId", "SubjectIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("StudentId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("SubjectId");

                            b1.HasKey("Id");

                            b1.HasIndex("StudentId");

                            b1.ToTable("StudentSubjectId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("AssessmentIds");

                    b.Navigation("ReportIds");

                    b.Navigation("SubjectIds");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.ContentDescriptor", b =>
                {
                    b.HasOne("Domain.SubjectAggregates.Entities.Strand", null)
                        .WithMany("_contentDescriptors")
                        .HasForeignKey("Strand")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SubjectAggregates.Entities.Substrand", null)
                        .WithMany("ContentDescriptors")
                        .HasForeignKey("Substrand")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Elaboration", b =>
                {
                    b.HasOne("Domain.SubjectAggregates.Entities.ContentDescriptor", null)
                        .WithMany("Elaborations")
                        .HasForeignKey("ContentDescriptor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Strand", b =>
                {
                    b.HasOne("Domain.SubjectAggregates.Entities.YearLevel", null)
                        .WithMany("Strands")
                        .HasForeignKey("YearLevel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Substrand", b =>
                {
                    b.HasOne("Domain.SubjectAggregates.Entities.Strand", null)
                        .WithMany("_substrands")
                        .HasForeignKey("Strand")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.YearLevel", b =>
                {
                    b.HasOne("Domain.SubjectAggregates.Subject", null)
                        .WithMany("YearLevels")
                        .HasForeignKey("Subject")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.TeacherAggregate.Teacher", b =>
                {
                    b.OwnsMany("Domain.StudentAggregate.ValueObjects.StudentId", "StudentIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("TeacherStudentId");

                            b1.HasKey("Id");

                            b1.HasIndex("TeacherId");

                            b1.ToTable("TeacherStudentId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.OwnsMany("Domain.Assessments.ValueObjects.AssessmentId", "AssessmentIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("TeacherAssessmentId");

                            b1.HasKey("Id");

                            b1.HasIndex("TeacherId");

                            b1.ToTable("TeacherAssessmentId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.OwnsMany("Domain.ReportAggregate.ValueObjects.ReportCommentId", "ReportIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("TeacherReportId");

                            b1.HasKey("Id");

                            b1.HasIndex("TeacherId");

                            b1.ToTable("TeacherReportId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.OwnsMany("Domain.ResourceAggregate.ValueObjects.ResourceId", "ResourceIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("TeacherResourceId");

                            b1.HasKey("Id");

                            b1.HasIndex("TeacherId");

                            b1.ToTable("TeacherResourceId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.OwnsMany("Domain.SubjectAggregates.ValueObjects.SubjectId", "SubjectIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("TeacherSubjectId");

                            b1.HasKey("Id");

                            b1.HasIndex("TeacherId");

                            b1.ToTable("TeacherSubjectId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.OwnsOne("Domain.UserAggregate.ValueObjects.UserId", "UserId", b1 =>
                        {
                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("UserId");

                            b1.HasKey("TeacherId");

                            b1.ToTable("Teachers");

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.Navigation("AssessmentIds");

                    b.Navigation("ReportIds");

                    b.Navigation("ResourceIds");

                    b.Navigation("StudentIds");

                    b.Navigation("SubjectIds");

                    b.Navigation("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.TermPlannerAggregate.TermPlanner", b =>
                {
                    b.OwnsMany("Domain.Common.Planner.ValueObjects.SchoolEventId", "SchoolEventIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("TermPlannerId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("SchoolEventId");

                            b1.HasKey("Id");

                            b1.HasIndex("TermPlannerId");

                            b1.ToTable("TermPlannerSchoolEventId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TermPlannerId");
                        });

                    b.OwnsMany("Domain.TimeTableAggregate.ValueObjects.WeekPlannerId", "WeekPlannerIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("TermPlannerId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("WeekPlannerI");

                            b1.HasKey("Id");

                            b1.HasIndex("TermPlannerId");

                            b1.ToTable("TermPlannerWeekPlannerId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TermPlannerId");
                        });

                    b.Navigation("SchoolEventIds");

                    b.Navigation("WeekPlannerIds");
                });

            modelBuilder.Entity("Domain.TimeTableAggregate.WeekPlanner", b =>
                {
                    b.OwnsMany("Domain.LessonPlanAggregate.ValueObjects.LessonPlanId", "LessonPlanIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("LessonPlanId");

                            b1.Property<Guid>("WeekPlannerId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("WeekPlannerId");

                            b1.ToTable("WeekPlannerLessonPlanId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WeekPlannerId");
                        });

                    b.OwnsMany("Domain.Common.Planner.ValueObjects.SchoolEventId", "SchoolEventIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<Guid>("Value")
                                .HasColumnType("char(36)")
                                .HasColumnName("SchoolEventId");

                            b1.Property<Guid>("WeekPlannerId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("WeekPlannerId");

                            b1.ToTable("WeekPlannerSchoolEventId", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WeekPlannerId");
                        });

                    b.Navigation("LessonPlanIds");

                    b.Navigation("SchoolEventIds");
                });

            modelBuilder.Entity("Domain.YearPlannerAggregate.YearPlanner", b =>
                {
                    b.OwnsMany("Domain.YearPlannerAggregate.Entities.TermPlan", "TermPlans", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Subjects")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<Guid>("YearPlannerId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("YearPlannerId");

                            b1.ToTable("TermPlan");

                            b1.WithOwner()
                                .HasForeignKey("YearPlannerId");
                        });

                    b.Navigation("TermPlans");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.ContentDescriptor", b =>
                {
                    b.Navigation("Elaborations");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Strand", b =>
                {
                    b.Navigation("_contentDescriptors");

                    b.Navigation("_substrands");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.Substrand", b =>
                {
                    b.Navigation("ContentDescriptors");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Entities.YearLevel", b =>
                {
                    b.Navigation("Strands");
                });

            modelBuilder.Entity("Domain.SubjectAggregates.Subject", b =>
                {
                    b.Navigation("YearLevels");
                });
#pragma warning restore 612, 618
        }
    }
}
