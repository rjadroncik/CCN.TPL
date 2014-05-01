Imports FluentNHibernate.Mapping
Imports FluentNHibernate.Conventions
Imports FluentNHibernate.Conventions.Instances
Imports FluentNHibernate.Conventions.Inspections

Public Class NamingConventions
    Implements IJoinedSubclassConvention
    Implements IReferenceConvention
    Implements IIdConvention
    Implements ICompositeIdentityConvention
    Implements IHasManyConvention
    Implements IHasManyToManyConvention

    Public Sub Apply(instance As IJoinedSubclassInstance) Implements IJoinedSubclassConvention.Apply

        Dim key As String = String.Format("FK_{0}_{1}", instance.EntityType.Name, instance.EntityType.BaseType.Name)

        instance.Key.ForeignKey(key)
    End Sub

    Public Sub Apply(instance As IManyToOneInstance) Implements IReferenceConvention.Apply

        Dim key As String = String.Format("FK_{0}_{1}", instance.EntityType.Name, instance.Property.Name)

        instance.ForeignKey(key)
    End Sub

    Public Sub Apply(instance As IIdentityInstance) Implements IConvention(Of IIdentityInspector, IIdentityInstance).Apply

        Dim key As String = String.Format("PK_{0}", instance.EntityType.Name)

        instance.Column(key)
    End Sub

    Public Sub Apply(instance As IManyToManyCollectionInstance) Implements IConvention(Of IManyToManyCollectionInspector, IManyToManyCollectionInstance).Apply

        Dim key1 As String = String.Format("FK_{0}_{1}", instance.TableName, instance.EntityType.Name)

        instance.Key.ForeignKey(key1)

        Dim key2 As String = String.Format("FK_{0}_{1}", instance.TableName, instance.ChildType.Name)

        instance.Relationship.ForeignKey(key2)
    End Sub

    Public Sub Apply(instance As ICompositeIdentityInstance) Implements IConvention(Of ICompositeIdentityInspector, ICompositeIdentityInstance).Apply

        For Each keyMayToOne As IKeyManyToOneInstance In instance.KeyManyToOnes

            Dim key As String = String.Format("FK_{0}_{1}", instance.EntityType.Name, keyMayToOne.Class.Name)

            keyMayToOne.ForeignKey(key)
        Next
    End Sub

    Public Sub Apply(instance As IOneToManyCollectionInstance) Implements IConvention(Of IOneToManyCollectionInspector, IOneToManyCollectionInstance).Apply

        Dim key As String = String.Format("FK_{0}_{1}", instance.EntityType.Name, instance.ChildType.Name)

        instance.Key.ForeignKey(key)
    End Sub
End Class
