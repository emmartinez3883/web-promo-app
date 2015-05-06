Imports Microsoft.VisualBasic
Imports System.Web.HttpContext
Imports System.DirectoryServices.AccountManagement

Public Class Util

    Private Shared Function GetGroups(ByVal userName As String) As List(Of String)
        Dim result As List(Of String) = New List(Of String)

        ' establish domain context
        Dim yourDomain As PrincipalContext = New PrincipalContext(ContextType.Domain)

        ' find your user
        Dim user As UserPrincipal = UserPrincipal.FindByIdentity(yourDomain, "HQ\" & userName)

        ' if found - grab its groups
        If (user IsNot Nothing) Then
            Dim groups As PrincipalSearchResult(Of Principal) = user.GetAuthorizationGroups()
            ' iterate over all groups
            For Each p As Principal In groups
                ' make sure to add only group principals
                If (TypeOf p Is GroupPrincipal) Then
                    result.Add(p.Name)
                End If
            Next
        End If
        Return (result)
    End Function


    Public Shared Sub SetSessionGroups(ByVal user As String)
        Dim mygroups As List(Of String) = GetGroups(user)
        ' the list of cleared group names should match the list in the following select statement
        Current.Session("AuthPrismUser") = False

        If mygroups.Contains("PRISM_users") Then
            Current.Session("AuthPrismUser") = True
        End If

    End Sub

    Public Shared Sub SetUserName(ByVal username As String, ByRef displayname As String)
        Dim DC = New PrincipalContext(ContextType.Domain)
        Dim user = UserPrincipal.FindByIdentity(DC, username)
        displayname = user.DisplayName
    End Sub

End Class
