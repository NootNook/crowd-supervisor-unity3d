# Surveillance et détection des mouvements de foule sur Unity 3D

Le projet consiste à déployer des drones aériens pour surveiller des événements culturels et détecter tout mouvement de foule ou réaction de panique. Ces drones doivent être capables de communiquer entre eux et de s'adapter en fonction de la situation. Lorsqu'un drone détecte un mouvement suspect, il doit le signaler aux drones voisins et recentrer sa caméra sur la zone en question. Si nécessaire, les drones voisins peuvent réajuster temporairement le périmètre de leur zone de surveillance pour couvrir les espaces qui ne sont plus visibles par le drone focalisé sur le mouvement en cours. En cas de problème, un gestionnaire de drones terrestre peut envoyer des drones au sol pour contenir le mouvement de foule et identifier et arrêter l'élément perturbateur.

## Présentation vidéo

https://user-images.githubusercontent.com/67638224/211047237-a4e4ebfd-85f4-4c22-8eb0-2d9518529ce9.mp4

## Vue d'ensemble du système

![Représentation du projet](docs/images/intro.png)

## Implémentations

todo

## Architecture du projet

| Compartiments                                                       	| Descriptions                                                                                          	|
|---------------------------------------------------------------------	|-------------------------------------------------------------------------------------------------------	|
| [Materials](CrowdSupervisor/Assets/Materials/)                      	| Textures du projet                                                                                    	|
| [Plugins](CrowdSupervisor/Assets/Plugins/)                          	| Plugin de [ZeroMQ](https://zeromq.org/), une bibliothèque de messagerie asynchrone                    	|
| [Prefabs](CrowdSupervisor/Assets/Prefabs/)                          	| Différents éléments du projet (drone, personnage...)                                                  	|
| [Scenes](CrowdSupervisor/Assets/Scenes/)                            	| Plusieurs scènes de notre simulation                                                                  	|
| [Scripts](CrowdSupervisor/Assets/Scripts/)                          	| Emplacement de notre code                                                                             	|
| ↳ [Bridge](CrowdSupervisor/Assets/Scripts/Bridge)                   	| Pont de communication entre Unity3D C# et le traitement d'image en Python avec la bibliothèque OpenCV 	|
| ↳ [Core](CrowdSupervisor/Assets/Scripts/Bridge)                     	| Gestion des informations entre l'interface utilisateur et la détection des perturbateurs              	|
| ↳ [Entity](CrowdSupervisor/Assets/Scripts/Entity)                   	| Script pour les perturbateurs                                                                         	|
| ↳ [FSM Behaviours](CrowdSupervisor/Assets/Scripts/FSM%20Behaviours) 	| Machine d'états finie pour les mouvements foule                                                       	|
| ↳ [Ground Team](CrowdSupervisor/Assets/Scripts/Ground%20Team)       	| Implémentation de l'équipe sol (rovers)                                                               	|
| ↳ [UAV](CrowdSupervisor/Assets/Scripts/UAV)                         	| Gestion des drones et leurs implémentations                                                           	|
| ↳ [UI](CrowdSupervisor/Assets/Scripts/UI)                           	| Gestion de l'interface utilisateur                                                                    	|
| ↳ [Utils](CrowdSupervisor/Assets/Scripts/Utils)                     	| Composants supplémentaires pour fonctionner le système                                                	|
| [UI Design](CrowdSupervisor/Assets/UI%20Design/)                    	| Architecture de notre interface utilisateur                                                           	|
| [UI Toolkit](CrowdSupervisor/Assets/UI%20Toolkit/)                  	| Composantes principales de l'interface utilisateur                                                    	|
