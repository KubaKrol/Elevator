==============================================
Elevator project made for recruitment purposes
==============================================

===
What could be done better (if I had more time):

-When trying to ride the elevator with FPS controller, the controller starts to vibrating due to being pushed by the collider.
 Changing the rigidbody settings is not helping and I did not want to invest time in fixing this since it was not a part of the task.
 Easy fix could be to make FPSController child of the Cabin object when inside the Cabin, using OnStayTrigger(collider other) so the transform.position.y would update by itself.

-There is no AudioManager and all of the sounds are "HardCoded". This is a bad approach since we have no control over the audio. 
 Task description suggested to avoid using Singleton pattern and this is the reason i didnt want to invest more time in figuring audio controller.

-PhotoCell in the Elevator door is not working as it was supposed to. Task description insisted to use Animation and i had troubles with setting a blend between open and close.
 If I could write it in code it would be much easier - I don't have much experience with animations unfortunately, need to work on it.

-The main method in the "Elevator" script is too big and too complex.

===
What went right:

-Elevator system is more complex than the elevator from task description. Elevator from the project is adapted to work with multi-player games or in situations
 when there's more than just one user (could be NPC). It's imitating real-world elevator.

-Project can be launched Out-Of-Box. If you would want to use this elevator in your project, setting up would be very easy - If you want additional floors,
 you can simply duplicate the Doors and buttons, set their "Floor" in the inspector, and your elevator is ready to lift. 

===
Additional comments:

-I would be VERY grateful for code review, thank you - jakubkrol1994@gmail.com
