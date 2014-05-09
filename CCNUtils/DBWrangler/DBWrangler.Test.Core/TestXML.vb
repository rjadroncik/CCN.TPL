Imports System.Text
Imports CCN.Core.VB
Imports DBWrangler.Model.Schema.Datatypes
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.IO

<TestClass()>
Public Class TestXML

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Use TestInitialize to run code before running each test
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Use TestCleanup to run code after each test has run
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <TestMethod()>
    Public Sub TestSchemaIO()

        Dim fileOriginal As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test.xml"
        Dim fileReconstructed As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test_reconstructed.xml"

        Dim schemaOriginal As Schema = CreateSampleSchema()

        SchemaXmlWriter.Write(schemaOriginal, fileOriginal)

        Dim schemaReconstructed As Schema = SchemaXmlReader.Read(fileOriginal)

        SchemaXmlWriter.Write(schemaReconstructed, fileReconstructed)

        Assert.IsTrue(Comparing.EqualContent(fileOriginal, fileReconstructed))
    End Sub

    Private Shared Function CreateSampleSchema() As Schema

        Dim schema As New Schema()

        '----------------------------------------- USER --------------------------------------------
        Dim user As New Table(schema)

        user.Name = "user"

        Dim userId As New Column(user, New DTInt32())
        userId.Name = "id"
        userId.Identity = True
        userId.Nullable = False

        user.Columns.Add(userId)

        Dim userLogin As New Column(user, New DTString(32))
        userLogin.Name = "login"
        userLogin.Nullable = False

        user.Columns.Add(userLogin)

        Dim userPassword As New Column(user, New DTString(32))
        userPassword.Name = "password"
        userPassword.Nullable = False

        user.Columns.Add(userPassword)

        user.PrimaryKey = New KeyPrimary(user)
        user.PrimaryKey.Name = "PK_user"
        user.PrimaryKey.Columns.Add(userId)

        Dim ukUserLogin As New KeyUnique(user)
        ukUserLogin.Name = "UK_user_login"
        ukUserLogin.Columns.Add(userLogin)

        user.UniqueKeys.Add(ukUserLogin)

        schema.Tables.Add(user)

        '----------------------------------------- ROLE --------------------------------------------
        Dim role As New Table(schema)

        role.Name = "role"

        Dim roleId As New Column(role, New DTInt32())
        roleId.Name = "id"
        roleId.Identity = True
        roleId.Nullable = False

        role.Columns.Add(roleId)

        Dim roleName As New Column(role, New DTString(32))
        roleName.Name = "login"
        roleName.Nullable = False

        role.Columns.Add(roleName)

        role.PrimaryKey = New KeyPrimary(role)
        role.PrimaryKey.Name = "PK_role"
        role.PrimaryKey.Columns.Add(roleId)

        schema.Tables.Add(role)

        '-------------------------------------- USER -> ROLE ---------------------------------------
        Dim userRole As New Table(schema)

        userRole.Name = "user_role"

        Dim userRoleUserId As New Column(userRole, New DTInt32())
        userRoleUserId.Name = "user_id"
        userRoleUserId.Nullable = False

        userRole.Columns.Add(userRoleUserId)

        Dim userRoleRoleId As New Column(userRole, New DTInt32())
        userRoleRoleId.Name = "role_id"
        userRoleRoleId.Nullable = False

        userRole.Columns.Add(userRoleRoleId)

        userRole.PrimaryKey = New KeyPrimary(role)
        userRole.PrimaryKey.Name = "PK_user_role"
        userRole.PrimaryKey.Columns.Add(userRoleUserId)
        userRole.PrimaryKey.Columns.Add(userRoleRoleId)

        Dim fkUserRoleUser As New KeyForeign(userRole)
        fkUserRoleUser.Name = "FK_user_role_user"
        fkUserRoleUser.ReferencedKey = user.PrimaryKey
        fkUserRoleUser.Columns.Add(userRoleUserId, userId)

        userRole.ForeignKeys.Add(fkUserRoleUser)

        Dim fkUserRoleRole As New KeyForeign(userRole)
        fkUserRoleRole.Name = "FK_user_role_role"
        fkUserRoleRole.ReferencedKey = role.PrimaryKey
        fkUserRoleRole.Columns.Add(userRoleRoleId, roleId)

        userRole.ForeignKeys.Add(fkUserRoleRole)

        schema.Tables.Add(userRole)

        Return schema
    End Function

End Class
