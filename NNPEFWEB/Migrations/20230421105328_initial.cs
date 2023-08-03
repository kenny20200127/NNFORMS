using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ResetPasswordCode = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    command = table.Column<string>(nullable: true),
                    Appointment = table.Column<string>(nullable: true),
                    personnel = table.Column<bool>(nullable: false),
                    shipcheck = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    expireDate = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_banks",
                columns: table => new
                {
                    bankcode = table.Column<string>(nullable: false),
                    bankname = table.Column<string>(nullable: true),
                    branchname = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    cbn_code = table.Column<string>(nullable: true),
                    cbn_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_banks", x => x.bankcode);
                });

            migrationBuilder.CreateTable(
                name: "ef_branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    branchName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_commands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    commandName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_contactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(nullable: true),
                    Ship = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_contactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_control",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ship = table.Column<string>(nullable: true),
                    startdate = table.Column<DateTime>(nullable: false),
                    enddate = table.Column<DateTime>(nullable: false),
                    processingyear = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_control", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_entrymodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_entrymodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_localgovts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    lgaName = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_localgovts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_personalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankId = table.Column<int>(nullable: false),
                    formNumber = table.Column<string>(nullable: true),
                    serviceNumber = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    OtherName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Passport = table.Column<byte[]>(nullable: true),
                    NokPassport = table.Column<byte[]>(nullable: true),
                    AltNokPassport = table.Column<byte[]>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    religion = table.Column<string>(nullable: true),
                    gsm_number = table.Column<string>(nullable: true),
                    gsm_number2 = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    home_address = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    Bankcode = table.Column<string>(nullable: true),
                    bankbranch = table.Column<string>(nullable: true),
                    BankACNumber = table.Column<string>(nullable: true),
                    pfacode = table.Column<string>(nullable: true),
                    specialisation = table.Column<string>(nullable: true),
                    command = table.Column<string>(nullable: true),
                    branch = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    exittype = table.Column<string>(nullable: true),
                    DateEmpl = table.Column<DateTime>(nullable: true),
                    advanceDate = table.Column<DateTime>(nullable: true),
                    DateLeft = table.Column<DateTime>(nullable: true),
                    seniorityDate = table.Column<DateTime>(nullable: true),
                    runoutDate = table.Column<DateTime>(nullable: true),
                    nok_phone12 = table.Column<string>(nullable: true),
                    nok_phone22 = table.Column<string>(nullable: true),
                    AddressofAcommodation = table.Column<string>(nullable: true),
                    AcommodationStatus = table.Column<string>(nullable: true),
                    yearOfPromotion = table.Column<string>(nullable: true),
                    expirationOfEngagementDate = table.Column<DateTime>(nullable: true),
                    StateofOrigin = table.Column<string>(nullable: true),
                    LocalGovt = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    nok_address = table.Column<string>(nullable: true),
                    nok_relation = table.Column<string>(nullable: true),
                    nok_phone = table.Column<string>(nullable: true),
                    nok_email = table.Column<string>(nullable: true),
                    nok_nationalId = table.Column<string>(nullable: true),
                    nok_name = table.Column<string>(nullable: true),
                    nok_address2 = table.Column<string>(nullable: true),
                    nok_relation2 = table.Column<string>(nullable: true),
                    nok_phone2 = table.Column<string>(nullable: true),
                    nok_email2 = table.Column<string>(nullable: true),
                    nok_nationalId2 = table.Column<string>(nullable: true),
                    nok_name2 = table.Column<string>(nullable: true),
                    sp_name = table.Column<string>(nullable: true),
                    sp_phone = table.Column<string>(nullable: true),
                    sp_phone2 = table.Column<string>(nullable: true),
                    sp_email = table.Column<string>(nullable: true),
                    chid_name = table.Column<string>(nullable: true),
                    chid_name2 = table.Column<string>(nullable: true),
                    chid_name3 = table.Column<string>(nullable: true),
                    chid_name4 = table.Column<string>(nullable: true),
                    entry_mode = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    taxed = table.Column<string>(nullable: true),
                    gradelevel = table.Column<string>(nullable: true),
                    gradetype = table.Column<string>(nullable: true),
                    entitlement = table.Column<string>(nullable: true),
                    town = table.Column<string>(nullable: true),
                    accomm_type = table.Column<string>(nullable: true),
                    GBC = table.Column<string>(nullable: true),
                    GBC_Number = table.Column<string>(nullable: true),
                    aircrew_allow = table.Column<string>(nullable: true),
                    pilot_allow = table.Column<string>(nullable: true),
                    shift_duty_allow = table.Column<string>(nullable: true),
                    hazard_allow = table.Column<string>(nullable: true),
                    rent_subsidy = table.Column<string>(nullable: true),
                    SBC_allow = table.Column<string>(nullable: true),
                    special_forces_allow = table.Column<string>(nullable: true),
                    other_allow = table.Column<string>(nullable: true),
                    other_allowspecify = table.Column<string>(nullable: true),
                    NSITFcode = table.Column<string>(nullable: true),
                    NHFcode = table.Column<string>(nullable: true),
                    FGSHLS_loan = table.Column<string>(nullable: true),
                    car_loan = table.Column<string>(nullable: true),
                    welfare_loan = table.Column<string>(nullable: true),
                    NNNCS_loan = table.Column<string>(nullable: true),
                    NNMFBL_loan = table.Column<string>(nullable: true),
                    PPCFS_loan = table.Column<string>(nullable: true),
                    Anyother_Loan = table.Column<string>(nullable: true),
                    NSITFcodeYear = table.Column<string>(nullable: true),
                    NHFcodeYear = table.Column<string>(nullable: true),
                    FGSHLS_loanYear = table.Column<string>(nullable: true),
                    car_loanYear = table.Column<string>(nullable: true),
                    welfare_loanYear = table.Column<string>(nullable: true),
                    NNNCS_loanYear = table.Column<string>(nullable: true),
                    NNMFBL_loanYear = table.Column<string>(nullable: true),
                    PPCFS_loanYear = table.Column<string>(nullable: true),
                    Anyother_LoanYear = table.Column<string>(nullable: true),
                    classes = table.Column<int>(nullable: false),
                    div_off_name = table.Column<string>(nullable: true),
                    div_off_rank = table.Column<string>(nullable: true),
                    div_off_svcno = table.Column<string>(nullable: true),
                    div_off_date = table.Column<DateTime>(nullable: true),
                    hod_name = table.Column<string>(nullable: true),
                    hod_rank = table.Column<string>(nullable: true),
                    hod_svcno = table.Column<string>(nullable: true),
                    hod_date = table.Column<DateTime>(nullable: true),
                    cdr_name = table.Column<string>(nullable: true),
                    cdr_rank = table.Column<string>(nullable: true),
                    cdr_svcno = table.Column<string>(nullable: true),
                    cdr_date = table.Column<DateTime>(nullable: true),
                    qualification = table.Column<string>(nullable: true),
                    division = table.Column<string>(nullable: true),
                    payrollclass = table.Column<string>(nullable: true),
                    emolumentform = table.Column<string>(nullable: true),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    dateModify = table.Column<DateTime>(nullable: true),
                    dateVerify = table.Column<DateTime>(nullable: true),
                    verifyBy = table.Column<DateTime>(nullable: true),
                    dateconfirmed = table.Column<DateTime>(nullable: true),
                    confirmedBy = table.Column<string>(nullable: true),
                    appointment = table.Column<string>(nullable: true),
                    FormYear = table.Column<string>(nullable: true),
                    upload = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_personalInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_personalInfosHist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankId = table.Column<int>(nullable: false),
                    formNumber = table.Column<string>(nullable: true),
                    serviceNumber = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    OtherName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Passport = table.Column<byte[]>(nullable: true),
                    NokPassport = table.Column<byte[]>(nullable: true),
                    AltNokPassport = table.Column<byte[]>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    religion = table.Column<string>(nullable: true),
                    gsm_number = table.Column<string>(nullable: true),
                    gsm_number2 = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    home_address = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    Bankcode = table.Column<string>(nullable: true),
                    bankbranch = table.Column<string>(nullable: true),
                    BankACNumber = table.Column<string>(nullable: true),
                    pfacode = table.Column<string>(nullable: true),
                    specialisation = table.Column<string>(nullable: true),
                    command = table.Column<string>(nullable: true),
                    branch = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    exittype = table.Column<string>(nullable: true),
                    DateEmpl = table.Column<DateTime>(nullable: true),
                    advanceDate = table.Column<DateTime>(nullable: true),
                    DateLeft = table.Column<DateTime>(nullable: true),
                    seniorityDate = table.Column<DateTime>(nullable: true),
                    runoutDate = table.Column<DateTime>(nullable: true),
                    nok_phone12 = table.Column<string>(nullable: true),
                    nok_phone22 = table.Column<string>(nullable: true),
                    AddressofAcommodation = table.Column<string>(nullable: true),
                    AcommodationStatus = table.Column<string>(nullable: true),
                    yearOfPromotion = table.Column<string>(nullable: true),
                    expirationOfEngagementDate = table.Column<DateTime>(nullable: true),
                    StateofOrigin = table.Column<string>(nullable: true),
                    LocalGovt = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    nok_address = table.Column<string>(nullable: true),
                    nok_relation = table.Column<string>(nullable: true),
                    nok_phone = table.Column<string>(nullable: true),
                    nok_email = table.Column<string>(nullable: true),
                    nok_nationalId = table.Column<string>(nullable: true),
                    nok_name = table.Column<string>(nullable: true),
                    nok_address2 = table.Column<string>(nullable: true),
                    nok_relation2 = table.Column<string>(nullable: true),
                    nok_phone2 = table.Column<string>(nullable: true),
                    nok_email2 = table.Column<string>(nullable: true),
                    nok_nationalId2 = table.Column<string>(nullable: true),
                    nok_name2 = table.Column<string>(nullable: true),
                    sp_name = table.Column<string>(nullable: true),
                    sp_phone = table.Column<string>(nullable: true),
                    sp_phone2 = table.Column<string>(nullable: true),
                    sp_email = table.Column<string>(nullable: true),
                    chid_name = table.Column<string>(nullable: true),
                    chid_name2 = table.Column<string>(nullable: true),
                    chid_name3 = table.Column<string>(nullable: true),
                    chid_name4 = table.Column<string>(nullable: true),
                    entry_mode = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    taxed = table.Column<string>(nullable: true),
                    gradelevel = table.Column<string>(nullable: true),
                    gradetype = table.Column<string>(nullable: true),
                    entitlement = table.Column<string>(nullable: true),
                    town = table.Column<string>(nullable: true),
                    accomm_type = table.Column<string>(nullable: true),
                    GBC = table.Column<string>(nullable: true),
                    GBC_Number = table.Column<string>(nullable: true),
                    aircrew_allow = table.Column<string>(nullable: true),
                    pilot_allow = table.Column<string>(nullable: true),
                    shift_duty_allow = table.Column<string>(nullable: true),
                    hazard_allow = table.Column<string>(nullable: true),
                    rent_subsidy = table.Column<string>(nullable: true),
                    SBC_allow = table.Column<string>(nullable: true),
                    special_forces_allow = table.Column<string>(nullable: true),
                    other_allow = table.Column<string>(nullable: true),
                    other_allowspecify = table.Column<string>(nullable: true),
                    NSITFcode = table.Column<string>(nullable: true),
                    NHFcode = table.Column<string>(nullable: true),
                    FGSHLS_loan = table.Column<string>(nullable: true),
                    car_loan = table.Column<string>(nullable: true),
                    welfare_loan = table.Column<string>(nullable: true),
                    NNNCS_loan = table.Column<string>(nullable: true),
                    NNMFBL_loan = table.Column<string>(nullable: true),
                    PPCFS_loan = table.Column<string>(nullable: true),
                    Anyother_Loan = table.Column<string>(nullable: true),
                    NSITFcodeYear = table.Column<string>(nullable: true),
                    NHFcodeYear = table.Column<string>(nullable: true),
                    FGSHLS_loanYear = table.Column<string>(nullable: true),
                    car_loanYear = table.Column<string>(nullable: true),
                    welfare_loanYear = table.Column<string>(nullable: true),
                    NNNCS_loanYear = table.Column<string>(nullable: true),
                    NNMFBL_loanYear = table.Column<string>(nullable: true),
                    PPCFS_loanYear = table.Column<string>(nullable: true),
                    Anyother_LoanYear = table.Column<string>(nullable: true),
                    classes = table.Column<int>(nullable: false),
                    div_off_name = table.Column<string>(nullable: true),
                    div_off_rank = table.Column<string>(nullable: true),
                    div_off_svcno = table.Column<string>(nullable: true),
                    div_off_date = table.Column<DateTime>(nullable: true),
                    hod_name = table.Column<string>(nullable: true),
                    hod_rank = table.Column<string>(nullable: true),
                    hod_svcno = table.Column<string>(nullable: true),
                    hod_date = table.Column<DateTime>(nullable: true),
                    cdr_name = table.Column<string>(nullable: true),
                    cdr_rank = table.Column<string>(nullable: true),
                    cdr_svcno = table.Column<string>(nullable: true),
                    cdr_date = table.Column<DateTime>(nullable: true),
                    qualification = table.Column<string>(nullable: true),
                    division = table.Column<string>(nullable: true),
                    payrollclass = table.Column<string>(nullable: true),
                    emolumentform = table.Column<string>(nullable: true),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    dateModify = table.Column<DateTime>(nullable: true),
                    dateVerify = table.Column<DateTime>(nullable: true),
                    verifyBy = table.Column<DateTime>(nullable: true),
                    dateconfirmed = table.Column<DateTime>(nullable: true),
                    confirmedBy = table.Column<string>(nullable: true),
                    appointment = table.Column<string>(nullable: true),
                    FormYear = table.Column<string>(nullable: true),
                    upload = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_personalInfosHist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_PersonnelLogins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    svcNo = table.Column<string>(nullable: true),
                    rank = table.Column<string>(nullable: true),
                    payClass = table.Column<string>(nullable: true),
                    surName = table.Column<string>(nullable: true),
                    otheName = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    confirmPassword = table.Column<bool>(nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: true),
                    loginDate = table.Column<DateTime>(nullable: true),
                    expireDate = table.Column<DateTime>(nullable: true),
                    Appointment = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    openship = table.Column<int>(nullable: false),
                    spec = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_PersonnelLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_ranks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_relationships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_relationships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_shiplogins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rank = table.Column<string>(nullable: true),
                    payClass = table.Column<string>(nullable: true),
                    surName = table.Column<string>(nullable: true),
                    otheName = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    confirmPassword = table.Column<bool>(nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: true),
                    loginDate = table.Column<DateTime>(nullable: true),
                    expireDate = table.Column<DateTime>(nullable: true),
                    Appointment = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    openship = table.Column<int>(nullable: false),
                    command = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_shiplogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_ships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    shipName = table.Column<string>(nullable: true),
                    LandSea = table.Column<string>(nullable: true),
                    commandid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_ships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_specialisationareas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_specialisationareas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_states",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    geozoneid = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_states", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "ef_systeminfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comp_code = table.Column<string>(nullable: true),
                    comp_name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    SiteStatus = table.Column<int>(nullable: false),
                    opendate = table.Column<DateTime>(nullable: false),
                    closedate = table.Column<DateTime>(nullable: false),
                    hrlink = table.Column<string>(nullable: true),
                    town = table.Column<string>(nullable: true),
                    lg = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    box = table.Column<string>(nullable: true),
                    tel = table.Column<string>(nullable: true),
                    OfficersFormNo = table.Column<int>(nullable: false),
                    RatingsFormNo = table.Column<int>(nullable: false),
                    TrainingFormNo = table.Column<int>(nullable: false),
                    serveraddr = table.Column<string>(nullable: true),
                    serverport = table.Column<string>(nullable: true),
                    email_pword = table.Column<string>(nullable: true),
                    company_image = table.Column<byte[]>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: false),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_systeminfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MenuGroupId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_MenuGroups_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenus_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ef_relationships",
                columns: new[] { "Id", "description" },
                values: new object[,]
                {
                    { 1, "Mother" },
                    { 2, "Father" },
                    { 3, "son" },
                    { 4, "Daughter" },
                    { 5, "Brother" },
                    { 6, "Sister" },
                    { 7, "Wife" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Code",
                table: "Menus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuGroupId",
                table: "Menus",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_MenuId",
                table: "RoleMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_RoleId",
                table: "RoleMenus",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ef_banks");

            migrationBuilder.DropTable(
                name: "ef_branches");

            migrationBuilder.DropTable(
                name: "ef_commands");

            migrationBuilder.DropTable(
                name: "ef_contactUs");

            migrationBuilder.DropTable(
                name: "ef_control");

            migrationBuilder.DropTable(
                name: "ef_entrymodes");

            migrationBuilder.DropTable(
                name: "ef_localgovts");

            migrationBuilder.DropTable(
                name: "ef_personalInfos");

            migrationBuilder.DropTable(
                name: "ef_personalInfosHist");

            migrationBuilder.DropTable(
                name: "ef_PersonnelLogins");

            migrationBuilder.DropTable(
                name: "ef_ranks");

            migrationBuilder.DropTable(
                name: "ef_relationships");

            migrationBuilder.DropTable(
                name: "ef_shiplogins");

            migrationBuilder.DropTable(
                name: "ef_ships");

            migrationBuilder.DropTable(
                name: "ef_specialisationareas");

            migrationBuilder.DropTable(
                name: "ef_states");

            migrationBuilder.DropTable(
                name: "ef_systeminfos");

            migrationBuilder.DropTable(
                name: "RoleMenus");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MenuGroups");
        }
    }
}
