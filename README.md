# Surveillance et détection des mouvements de foule sur Unity 3D

Le projet consiste à déployer des drones aériens pour surveiller des événements culturels et détecter tout mouvement de foule ou réaction de panique. Ces drones doivent être capables de communiquer entre eux et de s'adapter en fonction de la situation. Lorsqu'un drone détecte un mouvement suspect, il doit le signaler aux drones voisins et recentrer sa caméra sur la zone en question. Si nécessaire, les drones voisins peuvent réajuster temporairement le périmètre de leur zone de surveillance pour couvrir les espaces qui ne sont plus visibles par le drone focalisé sur le mouvement en cours. En cas de problème, un gestionnaire de drones terrestre peut envoyer des drones au sol pour contenir le mouvement de foule et identifier et arrêter l'élément perturbateur.

## Présentation vidéo

https://user-images.githubusercontent.com/67638224/211047237-a4e4ebfd-85f4-4c22-8eb0-2d9518529ce9.mp4

## Vue d'ensemble du système

![Représentation du projet](docs/images/intro.png)

## Implémentations

### Utilisateur

| Tâches                                                                                                                          	| Implementer ? 	|
|---------------------------------------------------------------------------------------------------------------------------------	|---------------	|
| Visualiser plusieurs caméras de drônes sur 1 écran                                                                              	| ✅             	|
| Pouvoir alterner entre les caméras et les consulter individuellement à l’aide de contrôles (clavier/souris)                     	| ✅             	|
| Recevoir une alerte sur l’écran lorsqu’une anomalie est détectée (être incité à consulter la ou les caméra(s) correspondante(s) 	| ✅             	|
| Consulter le nombre d’anomalies en cours en temps réel                                                                          	| ✅             	|
| Ajouter/Retirer des éléments de foule au cours de la simulation à l’aide de commandes (clavier/souris)                           	| ❌             	|
| Ajouter/Retirer des éléments de foule perturbateurs au cours de la simulation à l’aide de commande (clavier/souris)             	| ❌             	|
| Déclencher manuellement des comportements anormaux dans la foule                                                                	| ❌             	|

### Drone aérien

| Tâches                                                                                                                                                                                    	| Implementer ? 	|
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|---------------	|
| Couvrir l’ensemble de sa zone attribuée avec sa caméra                                                                                                                                    	| ✅             	|
| Détecter tout mouvement de foule défini comme “suspect”                                                                                                                                   	| ✅             	|
| Envoyer un signal au gestionnaire des drones au sol afin que celui-ci mobilise les ressources disponibles                                                                                 	| ✅             	|
| Recevoir un signal du gestionnaire des drones au sol afin de prendre en compte l’état d’avancement de la tâche des drones au sol.                                                         	| ✅             	|
| Envoyer un signal aux drones possédant les zones de surveillance adjacente dans le cas où le mouvement se propagerait (définir un niveau de “risque” provenant d’une certaine direction). 	| ❌             	|
| Recevoir et interpréter un signal des drones voisins pour préparer                                                                                                                        	| ❌             	|
| Compter le nombre d’éléments de foule présents dans la zone de surveillance                                                                                                               	| ❌             	|
| Adapter sa caméra à la situation (ex: zoomer sur le mouvement pour avoir un rendu plus détaillé)                                                                                          	| ❌             	|
| Réadapter l’angle de sa caméra pour couvrir les zones potentiellement non surveillées par un drone voisin en cours d’analyse d’une anomalie                                               	| ❌             	|
| Surveiller une zone de façon mobile, en se déplaçant suivant un modèle défini et en cohérence avec les autres drones                                                                      	| ❌             	|
| Surveiller une zone comportant des reliefs et des obstacles                                                                                                                               	| ❌             	|

### Gestionnaire des drones au sol

| Tâches                                                                                                                                                                                                                                                                                                                                          	| Implementer ? 	|
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|---------------	|
| Recevoir le signal des drones aériens et assigner les ressources disponibles aux zones concernées                                                                                                                                                                                                                                               	| ✅             	|
| Envoyer l’état de la mobilisation des ressources aux drones aériens ayant fait la requête de ressources terrestres. (Ressources en cours de déplacement / indisponibles, problème résolu…)                                                                                                                                                      	| ❌             	|
| Rendre compte de l’état des ressources (combien sont mobilisées, dans quelle zone, combien sont disponibles)                                                                                                                                                                                                                                    	| ❌             	|
| Réassigner une tâche à une ressource mobilisée sur le point de revenir après avoir complété sa mission (neutralisation ou déplacement de foule). Prendre en compte l’optimisation de cette demande en fonction de la position de la ressource, celle des ressources au sol alentour (et leur état) et celle de la zone concernée à l’instant T. 	| ❌             	|

### Drone au sol

| Tâches                                                                                                                                                                                                                                                                                                     	| Implementer ? 	|
|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|---------------	|
| Se rendre sur la zone signalée par les drones aériens                                                                                                                                                                                                                                                      	|               	|
| Pouvoir communiquer son emplacement de façon régulière avec le gestionnaire de ressources au sol                                                                                                                                                                                                           	|               	|
| [Action : gestion de la foule] Attirer et régulariser les éléments de foules réagissant à la perturbation                                                                                                                                                                                                  	|               	|
| [Action : gestion de la foule] Émettre un signal aux drones aériens voisins pour connaître la zone voisine la plus “stable” et recevoir leur réponse                                                                                                                                                       	|               	|
| [Action : gestion de la foule] Déplacer les éléments de foule attirés dans la zone voisine la plus sécurisée                                                                                                                                                                                               	|               	|
| [Action : gestion de la foule] Être capable de se diriger vers une nouvelle zone proche pour rediriger une autre partie de la foule après que le gestionnaire de drones au sol lui en a donné l’ordre. (cf. tâche de réorganisation et d’optimisation des parcours dans le gestionnaire de drones au sol). 	|               	|
| [Action : gestion de la perturbation] Repérer l’élément perturbateur une fois arrivé sur place                                                                                                                                                                                                             	|               	|
| [Action : gestion de la perturbation] “Neutraliser” la perturbation (soit en faisant disparaître l’élément en question à son contact, soit en l’escortant en dehors du périmètre de l’événement.)                                                                                                          	|               	|
| [Action : gestion de la perturbation] Être capable de “neutraliser” plusieurs éléments perturbateurs si le gestionnaire de drones au sol lui en donne l’ordre. (cf. tâche de réorganisation et d’optimisation des parcours dans le gestionnaire de drones au sol).                                         	|               	|
| Envoyer un message au gestionnaire de drones au sol lorsque la tâche est terminée et que le trajet retour est commencé.                                                                                                                                                                                    	|               	|

### Elément de foule standard

| Tâches                                                                                                                                                                                      	| Implementer ? 	|
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|---------------	|
| Se déplacer de façon aléatoire                                                                                                                                                              	|               	|
| Pouvoir changer aléatoirement de vitesse en restant dans les bornes de la “norme”                                                                                                           	|               	|
| Réagir à l’approche d’un élément perturbateur (ex: changement brusque de direction ou de vitesse, etc.)                                                                                     	|               	|
| S’arrêter temporairement de façon aléatoire                                                                                                                                                 	|               	|
| Lors de sa réaction à un comportement suspect, pouvoir être perçu temporairement comme un élément perturbateur par les autres éléments de foule. Le but est ici de créer un effet “domino”. 	|               	|

### Elément de foule perturbateur

| Tâches                                                                                                	| Implementer ? 	|
|-------------------------------------------------------------------------------------------------------	|---------------	|
| Se déplacer de façon aléatoire                                                                        	|               	|
| Pouvoir changer aléatoirement de vitesse en restant dans les bornes de la “norme”                     	|               	|
| Créer une anomalie dans la foule alentour une fois son comportement suspect activé                    	|               	|
| Activer son comportement suspect un certain temps après avoir agi comme un élément de foule standard. 	|               	|
| Désactiver son comportement suspect après un certain temps                                            	|               	|

## Architecture du projet

| Compartiments                                                       	| Descriptions                                                                                          	|
|---------------------------------------------------------------------	|-------------------------------------------------------------------------------------------------------	|
| [Materials](CrowdSupervisor/Assets/Materials/)                      	| Textures du projet                                                                                    	|
| [Plugins](CrowdSupervisor/Assets/Plugins/)                          	| Plugin de [ZeroMQ](https://zeromq.org/), une bibliothèque de messagerie asynchrone                    	|
| [Prefabs](CrowdSupervisor/Assets/Prefabs/)                          	| Différents éléments préfabs du projet (drone, personnage...)                                                  	|
| [Scenes](CrowdSupervisor/Assets/Scenes/)                            	| Plusieurs scènes de notre simulation                                                                  	|
| [Scripts](CrowdSupervisor/Assets/Scripts/)                          	| Emplacement de notre code                                                                             	|
| ↳ [Bridge](CrowdSupervisor/Assets/Scripts/Bridge)                   	| Pont de communication entre Unity3D C# et le traitement d'image en Python avec la bibliothèque OpenCV 	|
| ↳ [Core](CrowdSupervisor/Assets/Scripts/Bridge)                     	| Gestion des informations entre l'interface utilisateur et la détection des perturbateurs              	|
| ↳ [Entity](CrowdSupervisor/Assets/Scripts/Entity)                   	| Script pour la détection de perturbateurs                                                                         	|
| ↳ [FSM Behaviours](CrowdSupervisor/Assets/Scripts/FSM%20Behaviours) 	| Machine à états finis pour la simulation de foule                                                       	|
| ↳ [Ground Team](CrowdSupervisor/Assets/Scripts/Ground%20Team)       	| Implémentation de l'équipe sol (rovers)                                                               	|
| ↳ [UAV](CrowdSupervisor/Assets/Scripts/UAV)                         	| Gestion des drones et leurs implémentations                                                           	|
| ↳ [UI](CrowdSupervisor/Assets/Scripts/UI)                           	| Gestion de l'interface utilisateur                                                                    	|
| ↳ [Utils](CrowdSupervisor/Assets/Scripts/Utils)                     	| Composants supplémentaires pour fonctionner le système                                                	|
| [UI Design](CrowdSupervisor/Assets/UI%20Design/)                    	| Architecture de notre interface utilisateur                                                           	|
| [UI Toolkit](CrowdSupervisor/Assets/UI%20Toolkit/)                  	| Composantes principales de l'interface utilisateur                                                    	|
