Objeto "Cutter":
- Implementado usando el paquete [parabox-CSG](https://github.com/karl-/pb_CSG)
- Este objeto contiene un script que corta un objeto con el tag "Cuttable" usando la mesh que tenga asignada
- La parte cortada tendrá la textura del objeto "Cutter"
- El mesh usado para el corte puede ser distinto del que se vea
  - El mesh en la base del prefab es el usado para cortar, mientras que el que se encuentra en CutterVisual es, como dice su nombre, meramente para efectos visuales de corte
- Es muy importante que ambos objetos esten con los tres valores de escala del transform en 1 y que el pivote esté en el centro del objeto
  - La razón para esto es que usa las meshes como tal en vez de los GameObjects para crear el corte
  - Si se quiere cambiar la escala de los modelos, hay que usar el scale factor en la importación del modelo

Objeto "Spawner":
- Contiene una lista de prefabs. Solo hace falta añadir variantes de pescado a dicha lista.