using Godot;
using System;

public class Player : KinematicBody
{
	[Export]
	float speed; 
	[Export]
	float mouseSens;
	Vector3 moveVector;

	//References to scene nodes
	Camera camera;
	Gun gun;
	public Gun Gun 
	{
		get {return gun;}
		set {gun = value;}
	}
	World world;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Getting the nodes
		camera = GetNode<Camera>("Camera");
		gun = camera.GetNode<Gun>("Gun");
		world = GetParent<World>();
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	//Input - called everytime engine gets input
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion mouseMotion)
		{
			camera.RotateX(Mathf.Deg2Rad(mouseMotion.Relative.y * mouseSens * -1));
			camera.RotationDegrees = new Vector3(Mathf.Clamp(camera.RotationDegrees.x, -75.0f, 75.0f), 0.0f, 0.0f);
			this.RotateY(Mathf.Deg2Rad(mouseMotion.Relative.x * mouseSens * -1));
		}
	}

	public override void _Process(float delta)
	{
		if(Input.IsActionPressed("ui_cancel"))
			GetTree().Quit();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		//input
		moveVector = new Vector3(0.0f, 0.0f, 0.0f);
		if(Input.IsActionPressed("ui_up"))
			moveVector -= camera.GlobalTransform.basis.z;
		if(Input.IsActionPressed("ui_down"))
			moveVector += camera.GlobalTransform.basis.z;
		if(Input.IsActionPressed("ui_left"))
			moveVector -= camera.GlobalTransform.basis.x;
		if(Input.IsActionPressed("ui_right"))
			moveVector += camera.GlobalTransform.basis.x;
		moveVector.y = 0.0f;
		moveVector = moveVector.Normalized();

		if(Input.IsActionPressed("ui_accept") && gun.canShoot)
		{
			Vector3 position = gun.Shoot();
			// world.Spawn(position);
		}
		//update

		MoveAndSlide(moveVector*speed);
	}
}
