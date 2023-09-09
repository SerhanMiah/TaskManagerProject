import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import { Router } from '@angular/router';


// task.model.ts
export interface TaskItem {
  id: number;
  title: string;
  description: string;
  isCompleted: boolean;
  dueDate: Date;
  project: Project;
  projectId: number;
  priority: Priority;
  priorityId: number;
  tag: Tag;
  tagId: number;
  comments: Comment[];
}

export interface Tag {
  id: number;
  name: string;
  tasks: TaskItem[];
}

export interface Project {
  id: number;
  name: string;
  tasks: TaskItem[];
}

export interface Priority {
  id: number;
  level: PriorityLevel;
  tasks: TaskItem[];
}

export enum PriorityLevel {
  High,
  Medium,
  Low
}

export interface Comment {
  id: number;
  content: string;
  datePosted: Date;
  task: TaskItem;
  taskItemId: number;
  postedBy: any;  
  postedById: string;
}
@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
    tasks: TaskItem[] = []; // Changed task to tasks to be more descriptive

    constructor(private router: Router) {}

    async ngOnInit(): Promise<void>  {
        try {
            const { data } = await axios.get('http://localhost:5263/api/task');
            if (!data) {
                console.error('Received no data from API.');
                return;
            }

            // Assign data to tasks property if $values property is valid
            if (data.$values && Array.isArray(data.$values)) {
                this.tasks = data.$values;
            } else {
                console.error('Data does not contain a valid $values array:', data);
            }
            console.log(data);
        
        } catch (error) {
            console.error('An error occurred while fetching tasks:', error);
        }
    }
}
