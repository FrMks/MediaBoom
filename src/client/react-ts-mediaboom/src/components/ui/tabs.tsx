import { Tabs as TabsPrimitive } from "@base-ui/react/tabs"
import { cva, type VariantProps } from "class-variance-authority"

import { cn } from "@/lib/utils"

function Tabs({ className, ...props }: TabsPrimitive.Root.Props) {
  return (
    <TabsPrimitive.Root
      data-slot="tabs"
      className={cn("flex flex-col gap-2", className)}
      {...props}
    />
  )
}

const tabsListVariants = cva("group/tabs-list flex w-fit items-center", {
  variants: {
    variant: {
      default: "rounded-lg bg-muted p-1 text-muted-foreground",
      line: "gap-8 bg-transparent",
    },
  },
  defaultVariants: {
    variant: "default",
  },
})

function TabsList({
  className,
  variant = "default",
  ...props
}: TabsPrimitive.List.Props & VariantProps<typeof tabsListVariants>) {
  return (
    <TabsPrimitive.List
      data-slot="tabs-list"
      data-variant={variant}
      className={cn(tabsListVariants({ variant }), className)}
      {...props}
    />
  )
}

function TabsTrigger({ className, ...props }: TabsPrimitive.Tab.Props) {
  return (
    <TabsPrimitive.Tab
      data-slot="tabs-trigger"
      className={cn(
        "relative inline-flex flex-1 items-center justify-center border border-transparent px-3 py-2 text-sm font-medium whitespace-nowrap text-muted-foreground outline-none transition-colors",
        "hover:text-foreground focus-visible:ring-3 focus-visible:ring-ring/50 disabled:pointer-events-none disabled:opacity-50 data-active:text-foreground",
        "in-data-[variant=default]:rounded-md in-data-[variant=default]:data-active:bg-background in-data-[variant=default]:data-active:shadow-sm",
        "in-data-[variant=line]:border-b-white/15 in-data-[variant=line]:pb-4 in-data-[variant=line]:text-lg in-data-[variant=line]:font-semibold in-data-[variant=line]:text-white/45",
        "in-data-[variant=line]:after:absolute in-data-[variant=line]:after:inset-x-0 in-data-[variant=line]:after:-bottom-px in-data-[variant=line]:after:h-0.5 in-data-[variant=line]:after:origin-center in-data-[variant=line]:after:scale-x-0 in-data-[variant=line]:after:rounded-full in-data-[variant=line]:after:bg-linear-to-r in-data-[variant=line]:after:from-yellow-400 in-data-[variant=line]:after:to-orange-500 in-data-[variant=line]:after:shadow-[0_0_16px_rgb(255_179_0_/_38%)] in-data-[variant=line]:after:transition-transform",
        "in-data-[variant=line]:data-active:text-amber-400 in-data-[variant=line]:data-active:after:scale-x-100",
        className
      )}
      {...props}
    />
  )
}

function TabsContent({ className, ...props }: TabsPrimitive.Panel.Props) {
  return (
    <TabsPrimitive.Panel
      data-slot="tabs-content"
      className={cn("outline-none", className)}
      {...props}
    />
  )
}

export { Tabs, TabsContent, TabsList, TabsTrigger }
