# Sliders

## ContentSlider

![](../.res/md/img/ContentSlider.gif)

```XAML
<UserControl.Resources>
    <converters:EnumToBooleanConverter x:Key="EnumToBooleanConverter"/>
</UserControl.Resources>

<controls:ContentSlider Name="ContentSlider" Orientation="Horizontal"
                        Width="300" Height="30">
    <Rectangle>
        <Rectangle.Fill>
            <LinearGradientBrush EndPoint="1,0.5" StartPoint="0,0.5">
                <GradientStop Offset="0.000" Color="Red"/>
                <GradientStop Offset="0.166" Color="Yellow"/>
                <GradientStop Offset="0.333" Color="Lime"/>
                <GradientStop Offset="0.500" Color="Cyan"/>
                <GradientStop Offset="0.666" Color="Blue"/>
                <GradientStop Offset="0.833" Color="Magenta"/>
                <GradientStop Offset="1.000" Color="Red"/>
            </LinearGradientBrush>
        </Rectangle.Fill>
    </Rectangle>
</controls:ContentSlider>

<TextBlock Text="{Binding ElementName=ContentSlider, Path=Value}" HorizontalAlignment="Left" VerticalAlignment="Top"/>
```

## ContentPalette

![](../.res/md/img/ContentPalette.gif)

```XAML
<controls:ContentPalette Name="ContentPalette"
                        Width="100" Height="100">
    <Grid>
        <Rectangle Name="ColorRectangle" Fill="Red"/>
        <Rectangle Name="WhiteRectangle">
            <Rectangle.Fill>
                <LinearGradientBrush EndPoint="1,0" StartPoint="0,0">
                    <GradientStop Color="White" Offset="0"/>
                    <GradientStop Offset="1"/>
                </LinearGradientBrush>
            </Rectangle.Fill>
        </Rectangle>
        <Rectangle Name="BlackRectangle">
            <Rectangle.Fill>
                <LinearGradientBrush EndPoint="0,0" StartPoint="0,1">
                    <GradientStop Color="Black" Offset="0"/>
                    <GradientStop Offset="1" Color="#00000000"/>
                </LinearGradientBrush>
            </Rectangle.Fill>
        </Rectangle>
    </Grid>
</controls:ContentPalette>
<TextBlock HorizontalAlignment="Left" VerticalAlignment="Center" Margin="10,0">
    <Run Text="{Binding ElementName=ContentPalette, Path=Value.X, StringFormat={}X:{0}}"/>
    <LineBreak/>
    <Run Text="{Binding ElementName=ContentPalette, Path=Value.Y, StringFormat={}Y:{0}}"/>
</TextBlock>
```

## ContentCube

![](../.res/md/img/ContentCube.gif)

```XAML
<UserControl.Resources>
    <Viewport3D x:Key="brViewPort" Width="100" Height="100" HorizontalAlignment="Right" Margin="30">
        <Viewport3D.Camera>
            <OrthographicCamera Position="0,0,70"
                                LookDirection="0,0,-1"
                                UpDirection="0,1,0"
                                FarPlaneDistance="100" NearPlaneDistance="1"/>
        </Viewport3D.Camera>
        <ModelVisual3D>
            <ModelVisual3D.Content>
                <DirectionalLight Color="White" Direction="0,0,-1"/>
            </ModelVisual3D.Content>
        </ModelVisual3D>
        <ModelVisual3D>
            <ModelVisual3D.Content>
                <AmbientLight Color="Blue"/>
            </ModelVisual3D.Content>
        </ModelVisual3D>
        <ModelVisual3D>
            <ModelVisual3D.Content>
                <GeometryModel3D>
                    <GeometryModel3D.Geometry>
                        <MeshGeometry3D Positions="-1,-1,0 1,-1,0 1,1,0 -1,1,0"
                                        TriangleIndices="0,1,2 2,3,0"
                                        TextureCoordinates="1 0,0 0,0 1,1 1"
                                        Normals="0,1,0 0,1,0"/>
                    </GeometryModel3D.Geometry>
                    <GeometryModel3D.Material>
                        <DiffuseMaterial>
                            <DiffuseMaterial.Brush>
                                <LinearGradientBrush EndPoint="1,1" StartPoint="0,1">
                                    <GradientStop Color="Magenta"/>
                                    <GradientStop Color="Red" Offset="1"/>
                                </LinearGradientBrush>
                            </DiffuseMaterial.Brush>
                        </DiffuseMaterial>
                    </GeometryModel3D.Material>
                </GeometryModel3D>
            </ModelVisual3D.Content>
        </ModelVisual3D>
    </Viewport3D>
    <Viewport3D x:Key="brViewPort" Width="100" Height="100" HorizontalAlignment="Right" Margin="30">
        <!--Contents-->
    </Viewport3D>
    <Viewport3D x:Key="brViewPort" Width="100" Height="100" HorizontalAlignment="Right" Margin="30">
        <!--Contents-->
    </Viewport3D>
</UserControl.Resources>

<controls:ContentCube Name="ContentCube"
                      Width="200" Height="100"
                      XYContent="{StaticResource brViewPort}"
                      ZYContent="{StaticResource grViewPort}"
                      XZContent="{StaticResource bgViewPort}"
                      YOffset="{Binding RedValuePercent, Mode=TwoWay}">
    <controls:ContentCube.LayerContent>
        <Border Width="100" Height="100">
            <Border.Background>
                <VisualBrush>
                    <VisualBrush.Visual>
                        <Viewport3D x:Name="Layer" Width="60" Height="60" Margin="30,0">
                            <Viewport3D.Camera>
                                <OrthographicCamera Position="0,0,70"
                                                    LookDirection="0,0,-1"
                                                    UpDirection="0,1,0"
                                                    FarPlaneDistance="100" NearPlaneDistance="1"/>
                            </Viewport3D.Camera>
                            <ModelVisual3D>
                                <ModelVisual3D.Content>
                                    <DirectionalLight Color="White" Direction="0,0,-1"/>
                                </ModelVisual3D.Content>
                            </ModelVisual3D>
                            <ModelVisual3D>
                                <ModelVisual3D.Content>
                                    <AmbientLight Color="Yellow"/>
                                </ModelVisual3D.Content>
                            </ModelVisual3D>
                            <ModelVisual3D>
                                <ModelVisual3D.Content>
                                    <GeometryModel3D>
                                        <GeometryModel3D.Geometry>
                                            <MeshGeometry3D Positions="-1,-1,0 1,-1,0 1,1,0 -1,1,0"
                                                            TriangleIndices="0,1,2 2,3,0"
                                                            TextureCoordinates="1 0,0 0,0 1,1 1"
                                                            Normals="0,1,0 0,1,0"/>
                                        </GeometryModel3D.Geometry>
                                        <GeometryModel3D.Material>
                                            <DiffuseMaterial>
                                                <DiffuseMaterial.Brush>
                                                    <LinearGradientBrush EndPoint="1,1" StartPoint="0,1">
                                                        <!--(1,r,1)-->
                                                        <GradientStop Color="{Binding GreenMax}"/>
                                                        <!--(1,r,0)-->
                                                        <GradientStop Color="{Binding GreenMin}" Offset="1"/>
                                                    </LinearGradientBrush>
                                                </DiffuseMaterial.Brush>
                                            </DiffuseMaterial>
                                        </GeometryModel3D.Material>
                                    </GeometryModel3D>
                                </ModelVisual3D.Content>
                            </ModelVisual3D>
                        </Viewport3D>
                    </VisualBrush.Visual>
                </VisualBrush>
            </Border.Background>
        </Border>
    </controls:ContentCube.LayerContent>
</controls:ContentCube>

<TextBlock HorizontalAlignment="Left" VerticalAlignment="Center" Margin="10,0">
    <Run Text="{Binding ElementName=ContentCube, Path=Value.X, StringFormat={}X:{0}}"/>
    <LineBreak/>
    <Run Text="{Binding ElementName=ContentCube, Path=Value.Y, StringFormat={}Y:{0}}"/>
    <LineBreak/>
    <Run Text="{Binding ElementName=ContentCube, Path=Value.Z, StringFormat={}Z:{0}}"/>
</TextBlock>
```
