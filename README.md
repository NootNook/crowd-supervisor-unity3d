# Surveillance et détection des mouvements de foule sur Unity 3D

brouillon

![Représentation du projet](docs/images/intro.png)

<video width="640" height="360" controls>
  <source src="docs/videos/CrowdSupervisorV4.2.mp4" type="video/mp4">
</video>

Architecture du projet

| Compartiments                                                          	| Descriptions                                                                                          	|
|------------------------------------------------------------------------	|-------------------------------------------------------------------------------------------------------	|
| [Materials](CrowdSupervisor/Assets/Materials/)                         	| Textures du projet                                                                                    	|
| [Plugins](CrowdSupervisor/Assets/Plugins/)                             	| Plugin de [ZeroMQ](https://zeromq.org/), une bibliothèque de messagerie asynchrone                    	|
| [Prefabs](CrowdSupervisor/Assets/Prefabs/)                             	| Différents éléments du projet (drone, personnage...)                                                  	|
| [Scenes](CrowdSupervisor/Assets/Scenes/)                               	| Plusieurs scènes de notre simulation                                                                  	|
| [Scripts](CrowdSupervisor/Assets/Scripts/)                             	| Emplacement de notre code                                                                             	|
| > [Bridge](CrowdSupervisor/Assets/Scripts/Bridge)                   	| Pont de communication entre Unity3D C# et le traitement d'image en Python avec la bibliothèque OpenCV 	|
| > [Core](CrowdSupervisor/Assets/Scripts/Bridge)                     	| Gestion des informations entre l'interface utilisateur et la détection des perturbateurs              	|
| > [Entity](CrowdSupervisor/Assets/Scripts/Entity)                   	| Script pour les perturbateurs                                                                         	|
| > [FSM Behaviours](CrowdSupervisor/Assets/Scripts/FSM%20Behaviours) 	| Machine d'états finie pour les mouvements foule                                                       	|
| > [Ground Team](CrowdSupervisor/Assets/Scripts/Ground%20Team)       	| Implémentation de l'équipe sol (rovers)                                                               	|
| > [UAV](CrowdSupervisor/Assets/Scripts/UAV)                         	| Gestion des drones et leurs implémentations                                                           	|
| > [UI](CrowdSupervisor/Assets/Scripts/UI)                           	| Gestion de l'interface utilisateur                                                                    	|
| > [Utils](CrowdSupervisor/Assets/Scripts/Utils)                     	| Composants supplémentaires pour fonctionner le système                                                	|
| [UI Design](CrowdSupervisor/Assets/UI%20Design/)                       	| Architecture de notre interface utilisateur                                                           	|
| [UI Toolkit](CrowdSupervisor/Assets/UI%20Toolkit/)                     	| Composantes principales de l'interface utilisateur                                                    	|