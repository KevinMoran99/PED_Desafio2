Pues solo falta que se pueda eliminar un nodo del árbol y que se agregue a la pila de la derecha.
Podes basarte en el btnPop_Click para ver como desactivar los botones, iniciar la animación de que se muevan las letras (el segundo parámetro que le vas a pasar a ese método debe ser 550, en vez de 30) y para que el Push a la pila de la derecha espere a que la animación termine (la animación usa un timer, en teoría no debería ser necesario cambiar nada del evento tick para que lo tuyo funcione).
Queda a tu elección como seleccionar que nodo eliminar. Entre lo que se me ocurría era que 1) Se hiciera como en la guía, que metieras la letra a eliminar en un textbox y que de ahi la busque en el arbol, o que 2) Pusieras un checkbox/radiobutton o algo con el teclado que puse arriba, de manera que si esta checkeado que el teclado sirva para agregar letras a la primera pila, y que si no que sirva para eliminar la letra presionada del árbol. O como vos querás, si se te ocurre otra XD
Sacás también las screenshots.
