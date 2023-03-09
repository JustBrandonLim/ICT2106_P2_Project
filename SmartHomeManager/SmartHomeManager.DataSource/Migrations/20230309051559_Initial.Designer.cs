﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHomeManager.DataSource;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230309051559_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("SmartHomeManager.Domain.APIDomain.Entities.APIData", b =>
                {
                    b.Property<Guid>("APIDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("APIDataId");

                    b.ToTable("APIDatas");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.APIDomain.Entities.APIKey", b =>
                {
                    b.Property<string>("APIKeyType")
                        .HasColumnType("TEXT");

                    b.Property<string>("APILabelText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("APIKeyType");

                    b.ToTable("APIKeys");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.APIDomain.Entities.APIValue", b =>
                {
                    b.Property<string>("APIKeyType")
                        .HasColumnType("TEXT");

                    b.Property<string>("APIValues")
                        .HasColumnType("TEXT");

                    b.HasKey("APIKeyType");

                    b.ToTable("APIValues");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DevicesOnboarded")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Timezone")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TwoFactorFlag")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.DeviceProfile", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("TEXT");

                    b.HasKey("DeviceId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("DeviceProfiles");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProfileId");

                    b.HasIndex("AccountId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.CarbonFootprint", b =>
                {
                    b.Property<Guid>("CarbonFootprintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<double>("HouseholdConsumption")
                        .HasColumnType("REAL");

                    b.Property<string>("MonthOfAnalysis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CarbonFootprintId");

                    b.HasIndex("AccountId");

                    b.ToTable("CarbonFootprints");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.EnergyEfficiency", b =>
                {
                    b.Property<Guid>("EnergyEfficiencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<double>("EnergyEfficiencyIndex")
                        .HasColumnType("REAL");

                    b.HasKey("EnergyEfficiencyId");

                    b.HasIndex("DeviceId");

                    b.ToTable("EnergyEfficiency");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.ForecastChart", b =>
                {
                    b.Property<Guid>("ForecastChartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DateOfAnalysis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TimespanType")
                        .HasColumnType("INTEGER");

                    b.HasKey("ForecastChartId");

                    b.HasIndex("AccountId");

                    b.ToTable("ForecastCharts");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.ForecastChartData", b =>
                {
                    b.Property<Guid>("ForecastChartDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ForecastChartId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsForecast")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("ForecastChartDataId");

                    b.HasIndex("ForecastChartId");

                    b.ToTable("ForecastChartsData");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.Device", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceBrand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DevicePassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceSerialNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceTypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceWatts")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("TEXT");

                    b.HasKey("DeviceId");

                    b.HasIndex("AccountId");

                    b.HasIndex("DeviceSerialNumber")
                        .IsUnique();

                    b.HasIndex("DeviceTypeName");

                    b.HasIndex("RoomId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfiguration", b =>
                {
                    b.Property<string>("ConfigurationKey")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ConfigurationValue")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeviceBrand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConfigurationKey", "DeviceId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ConfigurationKey", "DeviceBrand", "DeviceModel");

                    b.ToTable("DeviceConfigurations");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfigurationLookUp", b =>
                {
                    b.Property<string>("ConfigurationKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceBrand")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConfigurationValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ValueMeaning")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConfigurationKey", "DeviceBrand", "DeviceModel");

                    b.ToTable("DeviceConfigurationLookUps");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceType", b =>
                {
                    b.Property<string>("DeviceTypeName")
                        .HasColumnType("TEXT");

                    b.HasKey("DeviceTypeName");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DeviceLog", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("TEXT");

                    b.Property<double>("DeviceActivity")
                        .HasColumnType("REAL");

                    b.Property<double>("DeviceEnergyUsage")
                        .HasColumnType("REAL");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DeviceState")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("TEXT");

                    b.HasKey("LogId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("RoomId");

                    b.ToTable("DeviceLogs");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceStoreDomain.Entities.DeviceProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductBrand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId");

                    b.ToTable("DeviceProducts");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DirectorDomain.Entities.History", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceAdjustedConfiguration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RuleHistoryId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("HistoryId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("RuleHistoryId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DirectorDomain.Entities.RuleHistory", b =>
                {
                    b.Property<Guid>("RuleHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("APIKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApiValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceConfiguration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RuleActionTrigger")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RuleEndTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RuleId")
                        .HasColumnType("TEXT");

                    b.Property<int>("RuleIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RuleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RuleStartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScenarioName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RuleHistoryId");

                    b.ToTable("RuleHistories");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.EnergyProfileDomain.Entities.EnergyProfile", b =>
                {
                    b.Property<Guid>("EnergyProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConfigurationDesc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ConfigurationValue")
                        .HasColumnType("INTEGER");

                    b.HasKey("EnergyProfileId");

                    b.HasIndex("AccountId");

                    b.ToTable("EnergyProfiles");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.HomeSecurityDomain.Entities.HomeSecurity", b =>
                {
                    b.Property<Guid>("HomeSecurityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SecurityModeState")
                        .HasColumnType("INTEGER");

                    b.HasKey("HomeSecurityId");

                    b.ToTable("HomeSecurities");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.HomeSecurityDomain.Entities.HomeSecurityDeviceDefinition", b =>
                {
                    b.Property<string>("DeviceGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConfigurationKey")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ConfigurationOffValue")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationOnValue")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeviceGroup");

                    b.ToTable("HomeSecurityDeviceDefinitions");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.HomeSecurityDomain.Entities.HomeSecuritySetting", b =>
                {
                    b.Property<Guid>("HomeSecuritySettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceGroup")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("HomeSecurityId")
                        .HasColumnType("TEXT");

                    b.HasKey("HomeSecuritySettingId");

                    b.HasIndex("DeviceGroup");

                    b.HasIndex("HomeSecurityId");

                    b.ToTable("HomeSecuritySettings");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.NotificationDomain.Entities.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("NotificationMessage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("TEXT");

                    b.HasKey("NotificationId");

                    b.HasIndex("AccountId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.DeviceCoordinate", b =>
                {
                    b.Property<Guid>("DeviceCoordinateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XCoordinate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YCoordinate")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeviceCoordinateId");

                    b.HasIndex("DeviceId")
                        .IsUnique();

                    b.ToTable("DeviceCoordinates");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RoomId");

                    b.HasIndex("AccountId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.RoomCoordinate", b =>
                {
                    b.Property<Guid>("RoomCoordinateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XCoordinate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YCoordinate")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoomCoordinateId");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("RoomCoordinates");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Rule", b =>
                {
                    b.Property<Guid>("RuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("APIKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionTrigger")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApiValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConfigurationKey")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ConfigurationValue")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("RuleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("RuleId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ScenarioId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Scenario", b =>
                {
                    b.Property<Guid>("ScenarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ScenarioName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("isActive")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScenarioId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Scenarios");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Troubleshooter", b =>
                {
                    b.Property<Guid>("TroubleshooterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Recommendation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TroubleshooterId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Troubleshooters");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.APIDomain.Entities.APIValue", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.APIDomain.Entities.APIKey", "APIKey")
                        .WithMany()
                        .HasForeignKey("APIKeyType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("APIKey");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.DeviceProfile", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Profile", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.CarbonFootprint", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.EnergyEfficiency", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.ForecastChart", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AnalysisDomain.Entities.ForecastChartData", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AnalysisDomain.Entities.ForecastChart", "ForecastChart")
                        .WithMany()
                        .HasForeignKey("ForecastChartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForecastChart");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.Device", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceType", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.RoomDomain.Entities.Room", "Room")
                        .WithMany("Devices")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Account");

                    b.Navigation("DeviceType");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfiguration", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.DeviceConfigurationLookUp", "DeviceConfigurationLookUp")
                        .WithMany()
                        .HasForeignKey("ConfigurationKey", "DeviceBrand", "DeviceModel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("DeviceConfigurationLookUp");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DeviceLog", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.RoomDomain.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DirectorDomain.Entities.History", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.DirectorDomain.Entities.RuleHistory", "RuleHistory")
                        .WithMany()
                        .HasForeignKey("RuleHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("RuleHistory");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.EnergyProfileDomain.Entities.EnergyProfile", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.HomeSecurityDomain.Entities.HomeSecuritySetting", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.HomeSecurityDomain.Entities.HomeSecurityDeviceDefinition", "HomeSecurityDeviceDefinition")
                        .WithMany()
                        .HasForeignKey("DeviceGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.HomeSecurityDomain.Entities.HomeSecurity", "HomeSecurity")
                        .WithMany()
                        .HasForeignKey("HomeSecurityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HomeSecurity");

                    b.Navigation("HomeSecurityDeviceDefinition");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.NotificationDomain.Entities.Notification", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.DeviceCoordinate", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithOne("DeviceCoordinate")
                        .HasForeignKey("SmartHomeManager.Domain.RoomDomain.Entities.DeviceCoordinate", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.Room", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.RoomCoordinate", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.RoomDomain.Entities.Room", "Room")
                        .WithOne("RoomCoordinate")
                        .HasForeignKey("SmartHomeManager.Domain.RoomDomain.Entities.RoomCoordinate", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Rule", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHomeManager.Domain.SceneDomain.Entities.Scenario", "Scenario")
                        .WithMany()
                        .HasForeignKey("ScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Scenario");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Scenario", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.AccountDomain.Entities.Profile", "Profile")
                        .WithMany("Scenarios")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.SceneDomain.Entities.Troubleshooter", b =>
                {
                    b.HasOne("SmartHomeManager.Domain.DeviceDomain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.AccountDomain.Entities.Profile", b =>
                {
                    b.Navigation("Scenarios");
                });

            modelBuilder.Entity("SmartHomeManager.Domain.DeviceDomain.Entities.Device", b =>
                {
                    b.Navigation("DeviceCoordinate")
                        .IsRequired();
                });

            modelBuilder.Entity("SmartHomeManager.Domain.RoomDomain.Entities.Room", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("RoomCoordinate")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
